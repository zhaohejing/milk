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
              #region 综合集成与管控系统
             new PermissionDefinition(StaticPermissionsName.Page_Integrated,
             "综合集成与管控系统","综合集成与管控系统")
             {
                 Childs = new List<PermissionDefinition>()
                 {
                     new PermissionDefinition(StaticPermissionsName.Page_Integrated_Manage,"首页"),
                     new PermissionDefinition(StaticPermissionsName.Page_Integrated_Structure,"结构监测"),
                     new PermissionDefinition(StaticPermissionsName.Page_Integrated_Data3,"数据管理3")
                     {
                         Childs = new List<PermissionDefinition>()
                         {
                            new PermissionDefinition(StaticPermissionsName.Page_Integrated_Data3_UserLogin,"用户登录"),
                            new PermissionDefinition(StaticPermissionsName.Page_Integrated_Data3_UserList,"用户列表"),
                         }
                     },
                 }
             },
              #endregion 
              #region 运营管理系统
    new PermissionDefinition(StaticPermissionsName.Page_Operation,
             "运营管理系统","运营管理系统")
    {
        Childs = new List<PermissionDefinition>()
        {
             #region 管廊信息
            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base,"管廊基本信息")
            {
                Childs = new List<PermissionDefinition>()
                {

   new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Pipe,"管廊信息")
                    {
                        Childs = new List<PermissionDefinition>()
                        {
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Pipe_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Pipe_Chart,"图表"),
                        }
                    },
                     new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_IntoPipe,"入廊管线")
                    {
                        Childs = new List<PermissionDefinition>()
                        {
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_IntoPipe_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_IntoPipe_Edit,"编辑"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_IntoPipe_Delete,"删除"),
                        }
                    },
                        new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Gallery,"入廊企业")
                    {
                        Childs = new List<PermissionDefinition>()
                        {
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Gallery_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Gallery_Chart,"图表"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Base_Gallery_Delete,"删除"),
                        }
                    }
                }

            }
              #endregion
             #region 档案管理
            ,new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc,"档案管理")
            {
                Childs = new List<PermissionDefinition>()
                {
                       new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Record,"管廊档案")
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Record_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Record_Edit,"编辑"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Record_Delete,"删除"),
                           }
                       },
                       new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Oval,"采购计划")  {
                           Childs = new List<PermissionDefinition>()
                           {
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Oval_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Oval_Chart,"图表"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Oval_Delete,"删除"),
                           }
                       },
                       new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Equipment,"设备台账")  {
                           Childs = new List<PermissionDefinition>()
                           {
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Equipment_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Equipment_Edit,"编辑"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Doc_Equipment_Delete,"删除"),
                           }
                       },
                }
            }
             
                 
                	#endregion
             #region 成本管理
             ,new PermissionDefinition(StaticPermissionsName.Page_Operation_Cost,"成本管理")
            {
                Childs = new List<PermissionDefinition>()
                {
                     new PermissionDefinition(StaticPermissionsName.Page_Operation_Cost_Create,"新增"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Cost_Edit,"编辑"),
                            new PermissionDefinition(StaticPermissionsName.Page_Operation_Cost_Delete,"删除"),
                }
            }
            	#endregion
             #region 收费管理
              ,new PermissionDefinition(StaticPermissionsName.Page_Operation_Charge,"收费管理")
	        #endregion
          },

        },
    #endregion
              #region 应急抢险系统
       new PermissionDefinition(StaticPermissionsName.Page_Emergency, "应急抢险系统","应急抢险系统")
       {
           Childs = new List<PermissionDefinition>()
           {
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_OneMap,"应急一张图"),
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module,"应急模块配置")
               {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module_Team,"应急救援队伍"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module_Part,"应急指挥部"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module_Material,"应急救援物质"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module_Place,"应急避难场所"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module_DealPro,"一般处置程序"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Module_Plan,"应急预案处理"),
                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_Trouble,"管廊隐患排查") {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Trouble_Up,"隐患自查上报"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Trouble_Deal,"隐患自查处理"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Trouble_Record,"隐患历史信息"),
                      
                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_Connet,"应急通讯录") {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Connet_Cate,"通讯录类别维护"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Connet_Manage,"通讯录维护"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Connet_View,"通讯录查看"),

                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_Map,"地图应急信息"){
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Map_Part,"应急指挥保障"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Map_Team,"应急队伍保障"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Map_Material,"应急物资保障"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Map_Place,"应急场所保障"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Map_Expert,"应急专家保障"),

                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_Duty,"应急值守")
               {
                   Childs = new List<PermissionDefinition>()
                   {
                         new PermissionDefinition(StaticPermissionsName.Page_Emergency_Duty_Create,"添加"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Duty_Edit,"编辑"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Duty_Delete,"删除"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Duty_View,"查看"),
                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Emergency_Drill,"应急演练")
               {
                  Childs = new List<PermissionDefinition>()
                   {
                         new PermissionDefinition(StaticPermissionsName.Page_Emergency_Drill_Create,"添加"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Drill_Edit,"编辑"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Drill_Delete,"删除"),
                       new PermissionDefinition(StaticPermissionsName.Page_Emergency_Drill_View,"查看"),
                   }
               },
           }
       },
	#endregion
              #region 智能运维系统
       
                 new PermissionDefinition(StaticPermissionsName.Page_Maintenance,
             "智能运维系统","智能运维系统")
                 {
                       Childs = new List<PermissionDefinition>()
           {
               new PermissionDefinition(StaticPermissionsName.Page_Maintenance_TroubleTree,"故障树分析"),
               new PermissionDefinition(StaticPermissionsName.Page_Maintenance_Knowledges,"知识库"),
               new PermissionDefinition(StaticPermissionsName.Page_Maintenance_Experts,"专家坐席"),
               new PermissionDefinition(StaticPermissionsName.Page_Maintenance_WorkFlow,"工单管理"),
               new PermissionDefinition(StaticPermissionsName.Page_Maintenance_Operation,"运维人员管理"),
           }
                 },
	#endregion
              #region 系统配置管理
    new PermissionDefinition(StaticPermissionsName.Page_Config,
             "系统配置管理","系统配置管理",PermissionType.Control)      {
               Childs = new List<PermissionDefinition>()
           {
               new PermissionDefinition(StaticPermissionsName.Page_Config_Manage,"用户管理","",PermissionType.Control)
               {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_User,"用户信息列表","",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_User_Create,"创建","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_User_Edit,"编辑","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_User_Delete,"删除","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_User_Auth,"授权","",PermissionType.Control),
                           }
                       },
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_Role,"角色信息列表","",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_Role_Create,"创建","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_Role_Edit,"编辑","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_Role_Delete,"删除","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Manage_Role_Allow,"授权","",PermissionType.Control),
                           }
                       }
                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Config_Menu,"菜单管理","",PermissionType.Control)
               {
                   Childs =  new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Menu_Create,"创建","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Menu_Edit,"编辑","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Menu_Delete,"删除","",PermissionType.Control),
                   }
               },
               new PermissionDefinition(StaticPermissionsName.Page_Config_Org,"组织机构管理","",PermissionType.Control)
               {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Create,"创建","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Edit,"编辑","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Delete,"删除","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Enable,"启用","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Disable,"禁用","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Allow,"分配网格","",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Config_Org_Move,"移动","",PermissionType.Control),
                   }
               }
           }
                 },
	#endregion
                  
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
        //综合集成与管控系统
        public const string Page_Integrated = "page.integrated";
        //首页
        public const string Page_Integrated_Manage = "page.integrated.manage";
        //结构检测
        public const string Page_Integrated_Structure = "page.integrated.structure";
        //数据
        public const string Page_Integrated_Data3 = "page.integrated.data3";
        public const string Page_Integrated_Data3_UserLogin = "page.integrated.data3.userlogin";
        //public const string Page_Integrated_Data3_UserLogin_Create = "page.integrated.data3.userlogin.create";
        //public const string Page_Integrated_Data3_UserLogin_Edit = "page.integrated.data3.userlogin.eidt";
        //public const string Page_Integrated_Data3_UserLogin_Delete = "page.integrated.data3.userlogin.delete";
        public const string Page_Integrated_Data3_UserList = "page.integrated.data3.userlist";

        //运营管理系统
        public const string Page_Operation = "page.operation";

        public const string Page_Operation_Base = "page.operation.base";
        //管廊
        public const string Page_Operation_Base_Pipe = "page.operation.base.pipe";
        public const string Page_Operation_Base_Pipe_Create = "page.operation.base.pipe.create";
        public const string Page_Operation_Base_Pipe_Chart = "page.operation.base.pipe.chart";
        //入朗管线
        public const string Page_Operation_Base_IntoPipe = "page.operation.base.intopipe";
        public const string Page_Operation_Base_IntoPipe_Create = "page.operation.base.intopipe.create";
        public const string Page_Operation_Base_IntoPipe_Edit = "page.operation.base.intopipe.eidt";
        public const string Page_Operation_Base_IntoPipe_Delete = "page.operation.base.intopipe.delete";
        //入朗企业
        public const string Page_Operation_Base_Gallery = "page.operation.base.gallery";
        public const string Page_Operation_Base_Gallery_Create = "page.operation.base.gallery.create";
        public const string Page_Operation_Base_Gallery_Chart = "page.operation.base.gallery.eidt";
        public const string Page_Operation_Base_Gallery_Delete = "page.operation.base.gallery.delete";
        //档案管理
        public const string Page_Operation_Doc = "page.operation.doc";
        public const string Page_Operation_Doc_Record = "page.operation.base.record";
        public const string Page_Operation_Doc_Record_Create = "page.operation.base.record.create";
        public const string Page_Operation_Doc_Record_Edit = "page.operation.base.record.edit";
        public const string Page_Operation_Doc_Record_Delete = "page.operation.base.record.delete";

        public const string Page_Operation_Doc_Oval = "page.operation.base.oval";
        public const string Page_Operation_Doc_Oval_Create = "page.operation.base.oval.cteate";
        public const string Page_Operation_Doc_Oval_Chart = "page.operation.base.oval.chart";
        public const string Page_Operation_Doc_Oval_Delete = "page.operation.base.oval.delete";

        public const string Page_Operation_Doc_Equipment = "page.operation.base.equipment";
        public const string Page_Operation_Doc_Equipment_Create = "page.operation.base.equipment.create";
        public const string Page_Operation_Doc_Equipment_Edit = "page.operation.base.equipment.edit";
        public const string Page_Operation_Doc_Equipment_Delete = "page.operation.base.equipment.delete";
        //成本
        public const string Page_Operation_Cost = "page.operation.cost";
        public const string Page_Operation_Cost_Create = "page.operation.cost.create";
        public const string Page_Operation_Cost_Edit = "page.operation.cost.edit";
        public const string Page_Operation_Cost_Delete = "page.operation.cost.delete";
        //收费
        public const string Page_Operation_Charge = "page.operation.charge";

        //应急抢险系统
        public const string Page_Emergency = "page.emergency";
        public const string Page_Emergency_OneMap = "page.emergency.onemap";

        public const string Page_Emergency_Module = "page.emergency.module";
        public const string Page_Emergency_Module_Team = "page.emergency.module.team";
        public const string Page_Emergency_Module_Part = "page.emergency.module.part";
        public const string Page_Emergency_Module_Material = "page.emergency.module.material";
        public const string Page_Emergency_Module_Place = "page.emergency.module.place";
        public const string Page_Emergency_Module_DealPro = "page.emergency.module.dealpro";
        public const string Page_Emergency_Module_Plan = "page.emergency.module.plan";


        public const string Page_Emergency_Trouble = "page.emergency.trouble";
        public const string Page_Emergency_Trouble_Up = "page.emergency.trouble.up";
        public const string Page_Emergency_Trouble_Deal = "page.emergency.trouble.deal";
        public const string Page_Emergency_Trouble_Record = "page.emergency.trouble.record";

        public const string Page_Emergency_Connet = "page.emergency.connet";
        public const string Page_Emergency_Connet_Cate = "page.emergency.connet.cate";
        public const string Page_Emergency_Connet_Manage = "page.emergency.connet.manage";
        public const string Page_Emergency_Connet_View = "page.emergency.connet.view";

        public const string Page_Emergency_Map = "page.emergency.map";
        public const string Page_Emergency_Map_Part = "page.emergency.map.part";
        public const string Page_Emergency_Map_Team = "page.emergency.map.team";
        public const string Page_Emergency_Map_Material = "page.emergency.map.material";
        public const string Page_Emergency_Map_Place = "page.emergency.map.place";
        public const string Page_Emergency_Map_Expert = "page.emergency.map.expert";

        public const string Page_Emergency_Duty = "page.emergency.duty";
        public const string Page_Emergency_Duty_Create = "page.emergency.duty.create";
        public const string Page_Emergency_Duty_Edit = "page.emergency.duty.edit";
        public const string Page_Emergency_Duty_Delete = "page.emergency.duty.delete";
        public const string Page_Emergency_Duty_View = "page.emergency.duty.view";

        public const string Page_Emergency_Drill = "page.emergency.drill";
        public const string Page_Emergency_Drill_Create = "page.emergency.drill.create";
        public const string Page_Emergency_Drill_Edit = "page.emergency.drill.edit";
        public const string Page_Emergency_Drill_Delete = "page.emergency.drill.delete";
        public const string Page_Emergency_Drill_View = "page.emergency.drill.view";
        //智能运维系统
        public const string Page_Maintenance = "page.maintenance";
        public const string Page_Maintenance_TroubleTree = "page.maintenance.troubletree";
        public const string Page_Maintenance_Knowledges = "page.maintenance.knowledges";
        public const string Page_Maintenance_Experts = "page.maintenance.experts";
        public const string Page_Maintenance_WorkFlow = "page.maintenance.workflow";
        public const string Page_Maintenance_Operation = "page.maintenance.operation";
        //系统配置管理
        public const string Page_Config = "page.config";

        public const string Page_Config_Manage = "page.config.control";
        public const string Page_Config_Manage_User = "page.config.control.user";
        public const string Page_Config_Manage_User_Create = "page.config.control.user.create";
        public const string Page_Config_Manage_User_Edit = "page.config.control.user.edit";
        public const string Page_Config_Manage_User_Delete = "page.config.control.user.delete";
        public const string Page_Config_Manage_User_Auth = "page.config.control.user.auth";
        public const string Page_Config_Manage_Role = "page.config.control.role";
        public const string Page_Config_Manage_Role_Create = "page.config.control.role.create";
        public const string Page_Config_Manage_Role_Edit = "page.config.control.role.edit";
        public const string Page_Config_Manage_Role_Delete = "page.config.control.role.delete";
        public const string Page_Config_Manage_Role_Allow = "page.config.control.role.allow";

        public const string Page_Config_Menu = "page.config.menu";
        public const string Page_Config_Menu_Create = "page.config.menu.create";
        public const string Page_Config_Menu_Edit = "page.config.menu.edit";
        public const string Page_Config_Menu_Delete = "page.config.menu.delete";


        public const string Page_Config_Org = "page.config.org";
        public const string Page_Config_Org_Create = "page.config.org.create";
        public const string Page_Config_Org_Edit = "page.config.org.edit";
        public const string Page_Config_Org_Delete = "page.config.org.delete";
        public const string Page_Config_Org_Enable = "page.config.org.enable";
        public const string Page_Config_Org_Disable = "page.config.org.disable";
        public const string Page_Config_Org_Allow = "page.config.org.allow";
        public const string Page_Config_Org_Move = "page.config.org.move";
        //public const string Page_Config_Statistical = "page.config.statistical";
        //public const string Page_Config_Statistical_Login = "page.config.statistical.login";
        //public const string Page_Config_Statistical_List = "page.config.statistical.list";


    }
}
