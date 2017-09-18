using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using YT.Customers.Dtos;
using YT.Customers.Exporting;
using YT.Dto;
using YT.Models;
using YT.Promoters.Dtos;

namespace YT.Customers
{
    /// <summary>
    /// 客户表服务实现
    /// </summary>
    [AbpAuthorize]
    public class CustomerAppService : YtAppServiceBase, ICustomerAppService
    {
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly ICustomerListExcelExporter _customerListExcelExporter;
        private readonly IRepository<Promoter> _promoteRepository;
        private readonly IRepository<Card> _cardRepository;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerListExcelExporter"></param>
        /// <param name="promoteRepository"></param>
        /// <param name="cardRepository"></param>
        public CustomerAppService(IRepository<Customer, int> customerRepository
      , ICustomerListExcelExporter customerListExcelExporter, IRepository<Promoter> promoteRepository, IRepository<Card> cardRepository)
        {
            _customerRepository = customerRepository;
            _customerListExcelExporter = customerListExcelExporter;
            _promoteRepository = promoteRepository;
            _cardRepository = cardRepository;
        }


        #region 实体的自定义扩展方法
        /// <summary>
        /// 
        /// </summary>
        private IQueryable<Customer> CustomerRepositoryAsNoTrack => _customerRepository.GetAll().AsNoTracking();
        #endregion
        #region 客户表管理
        /// <summary>
        /// 根据查询条件获取客户表分页列表
        /// </summary>
        public async Task<PagedResultDto<CustomerListDto>> GetPagedCustomersAsync(GetCustomerInput input)
        {

            var query = _customerRepository.GetAllIncluding(c => c.Promoter);
            query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), c => c.CustomerName.Contains(input.Name))
                .WhereIf(!input.Mobile.IsNullOrWhiteSpace(), c => c.Mobile.Contains(input.Mobile))
                .WhereIf(!input.PromoterName.IsNullOrWhiteSpace(),
                    c => c.Promoter != null && c.Promoter.PromoterName.Contains(input.PromoterName));
            var customerCount = await query.CountAsync();

            var customers = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var customerListDtos = customers.MapTo<List<CustomerListDto>>();
            return new PagedResultDto<CustomerListDto>(
            customerCount,
            customerListDtos
            );
        }

        /// <summary>
        /// 通过Id获取客户表信息进行编辑或修改 
        /// </summary>
        public async Task<GetCustomerForEditOutput> GetCustomerForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetCustomerForEditOutput();
            var protomers = await _promoteRepository.GetAllListAsync(c => c.IsActive);
            CustomerEditDto customerEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _customerRepository.GetAsync(input.Id.Value);
                customerEditDto = entity.MapTo<CustomerEditDto>();
            }
            else
            {
                customerEditDto = new CustomerEditDto();
            }

            output.Customer = customerEditDto;
            if (protomers.Any())
            {
                output.Promoters = protomers.MapTo<List<PromoterListDto>>();
            }
            return output;
        }


        /// <summary>
        /// 通过指定id获取客户表ListDto信息
        /// </summary>
        public async Task<CustomerListDto> GetCustomerByIdAsync(EntityDto<int> input)
        {
            var entity = await _customerRepository.GetAsync(input.Id);

            return entity.MapTo<CustomerListDto>();
        }







        /// <summary>
        /// 新增或更改客户表
        /// </summary>
        public async Task CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerInput input)
        {
            if (input.CustomerEditDto.Id.HasValue)
            {
                await UpdateCustomerAsync(input.CustomerEditDto);
            }
            else
            {
                await CreateCustomerAsync(input.CustomerEditDto);
            }
        }

        /// <summary>
        /// 新增客户表
        /// </summary>
        protected virtual async Task<CustomerEditDto> CreateCustomerAsync(CustomerEditDto input)
        {
            var count = await _customerRepository.CountAsync(c => c.Account.Equals(input.Account));
            if (count > 0)
            {
                throw new AbpException("用户账号已存在");
            }

            var entity = input.MapTo<Customer>();
            if (!input.Card.IsNullOrWhiteSpace())
            {
                var card = await _cardRepository.FirstOrDefaultAsync(c => c.CardCode.Equals(input.Card));
                if (!card.IsUsed)
                {
                    entity.Balance = card.Money;
                    card.IsUsed = true;
                }
            }
            entity = await _customerRepository.InsertAsync(entity);
            return entity.MapTo<CustomerEditDto>();
        }

        /// <summary>
        /// 编辑客户表
        /// </summary>
        protected virtual async Task UpdateCustomerAsync(CustomerEditDto input)
        {

            if (input.Id != null)
            {
                var entity = await _customerRepository.GetAsync(input.Id.Value);
                var count =
                   await _customerRepository.CountAsync(
                        c => c.Account.Equals(input.Account) && input.Account != entity.Account);
                if (count > 0)
                {
                    throw new AbpException("用户账号已存在");
                }
                input.MapTo(entity);

                if (!input.Card.IsNullOrWhiteSpace())
                {
                    var card = await _cardRepository.FirstOrDefaultAsync(c => c.CardCode.Equals(input.Card));
                    if (!card.IsUsed)
                    {
                        entity.Balance = card.Money;
                        card.IsUsed = true;
                    }
                }


                await _customerRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 删除客户表
        /// </summary>
        public async Task DeleteCustomerAsync(EntityDto<int> input)
        {
            var customer =await _customerRepository.FirstOrDefaultAsync(c => c.Id == input.Id);
            if (customer.Balance>0)
            {
                    throw new AbpException("用户余额有值,无法删除");
            }
            customer.IsDeleted = true;
        }

        /// <summary>
        /// 批量删除客户表
        /// </summary>
        public async Task BatchDeleteCustomerAsync(List<int> input)
        {
            var customers = await _customerRepository.GetAllListAsync(s => input.Contains(s.Id));
            foreach (var customer in customers)
            {
                if (customer.Balance>0)
                {
                    continue;
                    
                }
                customer.IsDeleted = true;
            }
        }

        #endregion
        #region 客户表的Excel导出功能

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetCustomerToExcel()
        {
            var entities = await _customerRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<CustomerListDto>>();

            var fileDto = _customerListExcelExporter.ExportCustomerToFile(dtos);



            return fileDto;
        }


        #endregion










    }
}
