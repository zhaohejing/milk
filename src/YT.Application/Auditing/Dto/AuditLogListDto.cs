using System;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;

namespace YT.Auditing.Dto
{
    /// <summary>
    /// 日志dto
    /// </summary>
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogListDto : EntityDto<long>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 导入id
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? ImpersonatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExecutionDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomData { get; set; }
    }
}