using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Modules;
using YT.Authorization;
using YT.Managers;

namespace YT.MobileApp
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(YtModel), typeof(YtCoreModule))]
    public class MobileModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            //Adding authorization providers
        //    Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper mappings
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
