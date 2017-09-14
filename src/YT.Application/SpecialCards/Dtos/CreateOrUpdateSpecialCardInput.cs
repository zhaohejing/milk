                            
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.SpecialCards.Dtos
{
    /// <summary>
    /// 奶鲜卡新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateSpecialCardInput  
    {
    /// <summary>
    /// 奶鲜卡编辑Dto
    /// </summary>
		public SpecialCardEditDto  SpecialCardEditDto {get;set;}
 
    }
}
