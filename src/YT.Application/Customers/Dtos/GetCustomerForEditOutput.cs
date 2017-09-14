                            
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using YT.Models;
namespace YT.Customers.Dtos
{
	/// <summary>
    /// 用于添加或编辑 客户表时使用的DTO
    /// </summary>
  
    public class GetCustomerForEditOutput 
    {
 

	      /// <summary>
         /// Customer编辑状态的DTO
        /// </summary>
    public CustomerEditDto Customer{get;set;}


    }
}
