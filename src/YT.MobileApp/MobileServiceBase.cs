using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using YT.Authorization.Users;
using YT.Managers.MultiTenancy;
using YT.Managers.Users;
using YT.MultiTenancy;

namespace YT
{
    /// <summary>
    /// All application services in this application is derived from this class.
    /// We can add common application service methods here.
    /// </summary>
    public abstract class MobileServiceBase : ApplicationService
    { /// <summary>
      /// 
      /// </summary>
        public TenantManager TenantManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserManager UserManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected MobileServiceBase()
        {
            LocalizationSourceName = YtConsts.LocalizationSourceName;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("当前用户token 已失效!");
            }

            return user;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual User GetCurrentUser()
        {
            var user = UserManager.FindById(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("当前用户token 已失效!");
            }

            return user;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}