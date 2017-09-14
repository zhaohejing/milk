using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace YT.Models
{
    [Table("milk_card")]
   public class Card:CreationAuditedEntity,ISoftDelete
    {
        /// <summary>
        /// 充值卡 卡号
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否已使用
        /// </summary>
        public bool IsUsed { get; set; }
    }

    [Table("milk_specialcard")]
    public  class  SpecialCard:CreationAuditedEntity,ISoftDelete,IPassivable
    {
        /// <summary>
        /// 卡号  
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 卡密码
        /// </summary>
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
