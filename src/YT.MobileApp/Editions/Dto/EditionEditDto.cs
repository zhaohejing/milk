using System.ComponentModel.DataAnnotations;
using Abp.Application.Editions;
using Abp.AutoMapper;

namespace YT.MobileApp.Editions.Dto
{/// <summary>
 /// 
 /// </summary>
    [AutoMap(typeof(Edition))]
    public class EditionEditDto
    {/// <summary>
     /// 
     /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string DisplayName { get; set; }
    }
}