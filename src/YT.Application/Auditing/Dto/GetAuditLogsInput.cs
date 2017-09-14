using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Abp.Timing;
using YT.Dto;

namespace YT.Auditing.Dto
{/// <summary>
/// 
/// </summary>
    public class GetAuditLogsInput : PagedAndSortedInputDto, IShouldNormalize
    {/// <summary>
    /// 
    /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
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
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? HasException { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MinExecutionDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MaxExecutionDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ExecutionTime DESC";
            }

            if (Sorting.IndexOf("UserName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "User." + Sorting;
            }
            else
            {
                Sorting = "AuditLog." + Sorting;
            }
        }
    }
}