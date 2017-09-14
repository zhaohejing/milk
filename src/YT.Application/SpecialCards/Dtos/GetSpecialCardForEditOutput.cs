                            
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using YT.Models;
namespace YT.SpecialCards.Dtos
{
	/// <summary>
    /// 用于添加或编辑 奶鲜卡时使用的DTO
    /// </summary>
  
    public class GetSpecialCardForEditOutput 
    {
 

	      /// <summary>
         /// SpecialCard编辑状态的DTO
        /// </summary>
    public SpecialCardEditDto SpecialCard{get;set;}


    }
}
