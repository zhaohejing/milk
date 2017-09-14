                          
  
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.SpecialCards.Dtos
{
    /// <summary>
    /// 奶鲜卡编辑用Dto
    /// </summary>
    [AutoMap(typeof(SpecialCard))]
    public class SpecialCardEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public int? Id{get;set;}

        /// <summary>
        /// 卡号
        /// </summary>
        [DisplayName("卡号")]
        [Required]
        public   string  CardCode { get; set; }

        /// <summary>
        /// 卡密码
        /// </summary>
        [DisplayName("卡密码")]
        [Required]
        public   string  Password { get; set; }

        public   bool  IsActive { get; set; }

    }
}
