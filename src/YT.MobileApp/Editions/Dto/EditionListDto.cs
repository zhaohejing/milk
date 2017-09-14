using System;
using Abp.Application.Editions;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace YT.MobileApp.Editions.Dto
{/// <summary>
/// 
/// </summary>
    [AutoMapFrom(typeof(Edition))]
    public class EditionListDto : EntityDto, IHasCreationTime
    {/// <summary>
    /// 
    /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}