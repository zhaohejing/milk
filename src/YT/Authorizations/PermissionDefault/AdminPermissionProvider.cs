using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using YT.Navigations;

namespace YT.Authorizations.PermissionDefault
{
    public abstract class BasePermissionProvider : PermissionProvider
    {

    }
    public class AdminPermissionProvider : BasePermissionProvider
    {
        public override IEnumerable<PermissionDefinition> GetPermissionDefinitions(PermissionDefinitionProviderContext context)
        {
            return new List<PermissionDefinition>()
           {

               new PermissionDefinition(StaticPermissionsName.Page,"页面","鼻祖权限")
               {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Dashboard,"控制台","整体概述",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Customer,"客户管理","客户管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Customer_Client,"客户信息","客户信息管理",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Customer_Charge,"客户代充","客户代充",PermissionType.Control),
                           }
                       },
                          new PermissionDefinition(StaticPermissionsName.Page_Card,"卡片管理","卡片管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Card_Charge,"卡片管理","卡片管理",PermissionType.Control),
                           }
                       },
                            new PermissionDefinition(StaticPermissionsName.Page_Generalize,"推广管理","推广管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Card_Charge,"推广员管理","推广员管理",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Generalize_Wechat,"群发管理","群发管理",PermissionType.Control),
                           }
                       },
                              new PermissionDefinition(StaticPermissionsName.Page_System,"权限管理","权限管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_System_Role,"角色管理","角色管理",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_System_User,"账户管理","账户管理",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_System_Menu,"菜单管理","菜单管理",PermissionType.Control),
                           }
                       },
                                 new PermissionDefinition(StaticPermissionsName.Page_Log,"日志管理","日志管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Log_Audit,"日志查看","日志查看",PermissionType.Control)
                           }
                       },
                         new PermissionDefinition(StaticPermissionsName.Page_Statistics,"报表管理","日志管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_A,"Statistics_A","Statistics_A",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_B,"Statistics_B","Statistics_B",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_C,"Statistics_C","Statistics_C",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_D,"Statistics_D","Statistics_D",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_E,"Statistics_E","Statistics_E",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_F,"Statistics_F","Statistics_F",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_G,"Statistics_G","Statistics_G",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_H,"Statistics_H","Statistics_H",PermissionType.Control),
                           }
                       }

              }
               }
           };
        }
    }
    /// <summary>
    /// 静态权限名
    /// </summary>
    public class StaticPermissionsName
    {
        //默认首页权限
        public const string Page = "page";

        public const string Page_Dashboard = "page.dashboard";

        public const string Page_Customer = "page.customer";
        public const string Page_Customer_Client = "page.customer.client";
        public const string Page_Customer_Charge = "page.customer.charge";

        public const string Page_Card = "page.card";
        public const string Page_Card_Charge = "page.card.charge";

        public const string Page_Generalize = "page.generalize";
        public const string Page_Generalize_Promoters = "page.generalize.promoters";
        public const string Page_Generalize_Wechat = "page.generalize.wechat";

        public const string Page_System = "page.system";
        public const string Page_System_Role = "page.system.role";
        public const string Page_System_User = "page.system.user";
        public const string Page_System_Menu = "page.system.menu";

        public const string Page_Log = "page.log";
        public const string Page_Log_Audit = "page.log.audit";

        public const string Page_Statistics = "page.statistics";
        public const string Page_Statistics_A = "page.statistics.a";
        public const string Page_Statistics_B = "page.statistics.b";
        public const string Page_Statistics_C = "page.statistics.c";
        public const string Page_Statistics_D = "page.statistics.d";
        public const string Page_Statistics_E = "page.statistics.e";
        public const string Page_Statistics_F = "page.statistics.f";
        public const string Page_Statistics_G = "page.statistics.g";
        public const string Page_Statistics_H = "page.statistics.h";



    }
}
