using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.MobileApp.Editions.Dto;

namespace YT.MobileApp.Editions
{/// <summary>
/// 
/// </summary>
    public interface IEditionAppService : IApplicationService
    {/// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        Task<ListResultDto<EditionListDto>> GetEditions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEditionForEditOutput> GetEditionForEdit(NullableIdDto input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteEdition(EntityDto input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedEditionId"></param>
        /// <returns></returns>
        Task<List<ComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null);
    }
}