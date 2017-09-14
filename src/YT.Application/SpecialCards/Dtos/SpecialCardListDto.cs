                            
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;
namespace YT.SpecialCards.Dtos
{
	/// <summary>
    /// 奶鲜卡列表Dto
    /// </summary>
    [AutoMapFrom(typeof(SpecialCard))]
    public class SpecialCardListDto : EntityDto<int>
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [DisplayName("卡号")]
        public      string CardCode { get; set; }
        /// <summary>
        /// 卡密码
        /// </summary>
        [DisplayName("卡密码")]
        public      string Password { get; set; }
        public      bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
