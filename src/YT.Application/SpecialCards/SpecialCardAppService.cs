





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
using Abp.Linq.Extensions;
using YT.Dto;
using YT.Models;
using YT.SpecialCards.Dtos;
using YT.SpecialCards.Exporting;


namespace YT.SpecialCards
{
    /// <summary>
    /// 奶鲜卡服务实现
    /// </summary>
    [AbpAuthorize]


    public class SpecialCardAppService : YtAppServiceBase, ISpecialCardAppService
    {
        private readonly IRepository<SpecialCard, int> _specialCardRepository;
        private readonly ISpecialCardListExcelExporter _specialCardListExcelExporter;


        /// <summary>
        /// 构造方法
        /// </summary>
        public SpecialCardAppService(IRepository<SpecialCard, int> specialCardRepository,
              ISpecialCardListExcelExporter specialCardListExcelExporter
  )
        {
            _specialCardRepository = specialCardRepository;
            _specialCardListExcelExporter = specialCardListExcelExporter;
        }


        #region 实体的自定义扩展方法
        /// <summary>
        /// 
        /// </summary>
        private IQueryable<SpecialCard> SpecialCardRepositoryAsNoTrack => _specialCardRepository.GetAll().AsNoTracking();


        #endregion


        #region 奶鲜卡管理

        /// <summary>
        /// 根据查询条件获取奶鲜卡分页列表
        /// </summary>
        public async Task<PagedResultDto<SpecialCardListDto>> GetPagedSpecialCardsAsync(GetSpecialCardInput input)
        {

            var query = SpecialCardRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件

            var specialCardCount = await query.CountAsync();

            var specialCards = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var specialCardListDtos = specialCards.MapTo<List<SpecialCardListDto>>();
            return new PagedResultDto<SpecialCardListDto>(
            specialCardCount,
            specialCardListDtos
            );
        }

        /// <summary>
        /// 通过Id获取奶鲜卡信息进行编辑或修改 
        /// </summary>
        public async Task<GetSpecialCardForEditOutput> GetSpecialCardForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetSpecialCardForEditOutput();

            SpecialCardEditDto specialCardEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _specialCardRepository.GetAsync(input.Id.Value);
                specialCardEditDto = entity.MapTo<SpecialCardEditDto>();
            }
            else
            {
                specialCardEditDto = new SpecialCardEditDto();
            }

            output.SpecialCard = specialCardEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取奶鲜卡ListDto信息
        /// </summary>
        public async Task<SpecialCardListDto> GetSpecialCardByIdAsync(EntityDto<int> input)
        {
            var entity = await _specialCardRepository.GetAsync(input.Id);

            return entity.MapTo<SpecialCardListDto>();
        }







        /// <summary>
        /// 新增或更改奶鲜卡
        /// </summary>
        public async Task CreateOrUpdateSpecialCardAsync(CreateOrUpdateSpecialCardInput input)
        {
            if (input.SpecialCardEditDto.Id.HasValue)
            {
                await UpdateSpecialCardAsync(input.SpecialCardEditDto);
            }
            else
            {
                await CreateSpecialCardAsync(input.SpecialCardEditDto);
            }
        }

        /// <summary>
        /// 新增奶鲜卡
        /// </summary>
        protected virtual async Task<SpecialCardEditDto> CreateSpecialCardAsync(SpecialCardEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<SpecialCard>();

            entity = await _specialCardRepository.InsertAsync(entity);
            return entity.MapTo<SpecialCardEditDto>();
        }

        /// <summary>
        /// 编辑奶鲜卡
        /// </summary>
        protected virtual async Task UpdateSpecialCardAsync(SpecialCardEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            if (input.Id != null)
            {
                var entity = await _specialCardRepository.GetAsync(input.Id.Value);
                input.MapTo(entity);

                await _specialCardRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 删除奶鲜卡
        /// </summary>
        public async Task DeleteSpecialCardAsync(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _specialCardRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除奶鲜卡
        /// </summary>
        public async Task BatchDeleteSpecialCardAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _specialCardRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 奶鲜卡的Excel导出功能

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetSpecialCardToExcel()
        {
            var entities = await _specialCardRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<SpecialCardListDto>>();

            var fileDto = _specialCardListExcelExporter.ExportSpecialCardToFile(dtos);



            return fileDto;
        }


        #endregion










    }
}
