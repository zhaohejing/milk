using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace YT.MobileApp.Editions.Dto
{/// <summary>
/// 
/// </summary>
    public class GetEditionForEditOutput
    {/// <summary>
    /// 
    /// </summary>
        public EditionEditDto Edition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NameValueDto> FeatureValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FlatFeatureDto> Features { get; set; }
    }
}