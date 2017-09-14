using Abp.Application.Features;
using Abp.AutoMapper;
using Abp.UI.Inputs;

namespace YT.MobileApp.Editions.Dto
{/// <summary>
/// 
/// </summary>
    [AutoMapFrom(typeof(Feature))]
    public class FlatFeatureDto
    {/// <summary>
    /// 
    /// </summary>
        public string ParentName { get; set; }
        /// <summary>
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
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IInputType InputType { get; set; }
    }
}