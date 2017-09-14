





using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
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

        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomerAppService(IRepository<Customer, int> customerRepository
      , ICustomerListExcelExporter customerListExcelExporter
  )
        {
            _customerRepository = customerRepository;
            _customerListExcelExporter = customerListExcelExporter;
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

            var query = CustomerRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件

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
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Customer>();

            entity = await _customerRepository.InsertAsync(entity);
            return entity.MapTo<CustomerEditDto>();
        }

        /// <summary>
        /// 编辑客户表
        /// </summary>
        protected virtual async Task UpdateCustomerAsync(CustomerEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            if (input.Id != null)
            {
                var entity = await _customerRepository.GetAsync(input.Id.Value);
                input.MapTo(entity);

                await _customerRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 删除客户表
        /// </summary>
        public async Task DeleteCustomerAsync(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _customerRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除客户表
        /// </summary>
        public async Task BatchDeleteCustomerAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _customerRepository.DeleteAsync(s => input.Contains(s.Id));
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
