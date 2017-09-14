using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.MobileApp.Editions.Dto
{/// <summary>
/// 
/// </summary>
    public class CreateOrUpdateEditionDto
    {/// <summary>
    /// 
    /// </summary>
        [Required]
        public EditionEditDto Edition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public List<NameValueDto> FeatureValues { get; set; }
    }
}