using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using YT.Authorizations.PermissionDefault;

namespace YT.Navigations.MenuDefault
{
    public abstract class BaseMenuProvider : MenuProvider
    {

    }

    public class AdminMenuProvider : BaseMenuProvider
    {
        private int _seed = 0;
        public override IEnumerable<MenuDefinition> GetMenuDefinitions(MenuDefinitionProviderContext context)
        {
            return new List<MenuDefinition>()
           {
                    #region 综合集成与管控系统
        
         new MenuDefinition("综合集成与管控系统","","el-icon-date",true,StaticPermissionsName.Page_Integrated)
  {
      Childs = new List<MenuDefinition>()
      {
          new MenuDefinition( "首页","/manage","iconfont icon-all",true,StaticPermissionsName.Page_Integrated_Manage),
          new MenuDefinition( "结构监测系统","/integratedControlSystem/structureMonitor",
          "el-icon-document",true,StaticPermissionsName.Page_Integrated_Structure),

          new MenuDefinition( "数据管理3","","el-icon-document",true,StaticPermissionsName.Page_Integrated_Data3)
          {
              Childs = new List<MenuDefinition>()
              {
                   new MenuDefinition( "用户登录记录","/loginList","el-icon-document",true,StaticPermissionsName.Page_Integrated_Data3_UserLogin),
                   new MenuDefinition( "用户列表","/userList","el-icon-document",true,StaticPermissionsName.Page_Integrated_Data3_UserList),
              }
          },

      }
  }
	#endregion
      #region 运营管理系统
 
      ,  new MenuDefinition( "运营管理系统","","el-icon-plus",true,StaticPermissionsName.Page_Operation)
               {
                  Childs = new List<MenuDefinition>()
                   {
                       new MenuDefinition( "管廊基本信息管理","","el-icon-document",true,StaticPermissionsName.Page_Operation_Base)
                       {
                           Childs = new List<MenuDefinition>()
                           {
                               new MenuDefinition( "管廊信息管理","/operationSystem/pipeGalleryBaseInfo","",true,StaticPermissionsName.Page_Operation_Base_Pipe),
                               new MenuDefinition( "入廊管线管理","/operationSystem/intoPipeline","",true,StaticPermissionsName.Page_Operation_Base_IntoPipe),
                               new MenuDefinition( "入廊企业基本信息管理","/operationSystem/intoGalleryCompany","",true,StaticPermissionsName.Page_Operation_Base_Gallery),
                           }
                       },
                      new MenuDefinition( "档案管理","","el-icon-document",true,StaticPermissionsName.Page_Operation_Doc)
                       {
                           Childs = new List<MenuDefinition>()
                           {
                                new MenuDefinition( "管廊档案管理","/operationSystem/galleryRecord","",true,StaticPermissionsName.Page_Operation_Doc_Record),
                                new MenuDefinition( "设备采购计划及审批","/operationSystem/equipmentPurchasePlanApproval","",true,StaticPermissionsName.Page_Operation_Doc_Oval),
                                new MenuDefinition( "设备台账管理","/operationSystem/equipmentRecord","",true,StaticPermissionsName.Page_Operation_Doc_Equipment),
                           }
                       },
                       new MenuDefinition( "成本管理","/operationSystem/costManage","iconfont icon-all",true,StaticPermissionsName.Page_Operation_Cost),
                       new MenuDefinition( "收费管理","/operationSystem/chargeManage","iconfont icon-all",true,StaticPermissionsName.Page_Operation_Charge),

                   }
               }
	#endregion
#region 应急抢险系统
 
            ,
    new MenuDefinition( "应急抢险系统","","el-icon-information",true,StaticPermissionsName.Page_Emergency)
   {
       Childs = new EditableList<MenuDefinition>()
       {
     new MenuDefinition( "应急一张图","/emergencySystem/oneMap","iconfont icon-map",true,StaticPermissionsName.Page_Emergency_OneMap),
     new MenuDefinition( "应急模块配置","","iconfont icon-map",true,StaticPermissionsName.Page_Emergency_Module)
     {
         Childs = new List<MenuDefinition>()
         {
      new MenuDefinition( "应急救援队伍","/emergencySystem/rescueTeam","",true,StaticPermissionsName.Page_Emergency_Module_Team),
      new MenuDefinition( "应急指挥部","/emergencySystem/command","",true,StaticPermissionsName.Page_Emergency_Module_Part),
      new MenuDefinition( "应急救援物质","/emergencySystem/RescueMaterial","",true,StaticPermissionsName.Page_Emergency_Module_Material),
      new MenuDefinition( "应急避难场所","/emergencySystem/TakeRefuge","",true,StaticPermissionsName.Page_Emergency_Module_Place),
      new MenuDefinition( "一般处置程序","/emergencySystem/Management","",true,StaticPermissionsName.Page_Emergency_Module_DealPro),
      new MenuDefinition( "应急预案管理","/emergencySystem/PlanManagement","",true,StaticPermissionsName.Page_Emergency_Module_Plan),

         }
     },
       new MenuDefinition( "管廊隐患排查","","iconfont icon-map",true,StaticPermissionsName.Page_Emergency_Trouble)
     {
         Childs = new List<MenuDefinition>()
         {
      new MenuDefinition( "隐患自查上报","/emergencySystem/report","",true,StaticPermissionsName.Page_Emergency_Trouble_Up),
      new MenuDefinition( "隐患自查处理","/emergencySystem/handle","",true,StaticPermissionsName.Page_Emergency_Trouble_Deal),
      new MenuDefinition( "隐患历史信息","/emergencySystem/information","",true,StaticPermissionsName.Page_Emergency_Trouble_Record),

         }
     },
           new MenuDefinition( "应急通讯录管理","","iconfont icon-map",true,StaticPermissionsName.Page_Emergency_Connet)
     {
         Childs = new List<MenuDefinition>()
         {
      new MenuDefinition( "通讯录类别维护","/emergencySystem/listStyleMaintain","",true,StaticPermissionsName.Page_Emergency_Connet_Cate),
      new MenuDefinition( "通讯录维护","/emergencySystem/telListMaintain","",true,StaticPermissionsName.Page_Emergency_Connet_Manage),
      new MenuDefinition( "通讯录查看","/emergencySystem/telListLook","",true,StaticPermissionsName.Page_Emergency_Connet_View),

         }
     },
   new MenuDefinition( "地图应急信息","","iconfont icon-map",true,StaticPermissionsName.Page_Emergency_Map)
     {
         Childs = new List<MenuDefinition>()
         {
      new MenuDefinition( "应急指挥保障","/emergencySystem/yjzh","",true,StaticPermissionsName.Page_Emergency_Map_Part),
      new MenuDefinition( "应急队伍保障","/emergencySystem/yjdwbz","",true,StaticPermissionsName.Page_Emergency_Map_Team),
      new MenuDefinition( "应急物资保障","/emergencySystem/yjwzbz","",true,StaticPermissionsName.Page_Emergency_Map_Material),
      new MenuDefinition( "避难场所保障","/emergencySystem/bncsbz","",true,StaticPermissionsName.Page_Emergency_Map_Place),
      new MenuDefinition( "应急专家保障","/emergencySystem/yjzjbz","",true,StaticPermissionsName.Page_Emergency_Map_Expert),

         }
     },
      new MenuDefinition( "应急值守","/emergencySystem/onDuty","",true,StaticPermissionsName.Page_Emergency_Duty),
      new MenuDefinition( "应急演练","/emergencySystem/drilling","",true,StaticPermissionsName.Page_Emergency_Drill),

       }
   }
	#endregion
        #region 智能运维系统
 
   ,  new MenuDefinition( "智能运维系统","","",true,StaticPermissionsName.Page_Maintenance)
  {
      Childs = new List<MenuDefinition>()
      {
          new MenuDefinition( "故障树分析","","el-icon-document",true,StaticPermissionsName.Page_Maintenance_TroubleTree)
          {
              Childs = new List<MenuDefinition>()
              {
                   new MenuDefinition( "文件操作","/maintenanceSystem/documentOperation","el-icon-document",true,StaticPermissionsName.Page_Maintenance_TroubleTree),
                   new MenuDefinition( "故障分析","/maintenanceSystem/faultAnalysis","el-icon-document",true,StaticPermissionsName.Page_Maintenance_TroubleTree),
                   new MenuDefinition( "符号库管理","/maintenanceSystem/symbolManagement","el-icon-document",true,StaticPermissionsName.Page_Maintenance_TroubleTree),
              }
          },

          new MenuDefinition( "知识库","","el-icon-document",true,StaticPermissionsName.Page_Maintenance_Knowledges) {
              Childs = new List<MenuDefinition>()
              {
                   new MenuDefinition( "知识库管理","/maintenanceSystem/KBM","el-icon-document",true,StaticPermissionsName.Page_Maintenance_Knowledges),
                   new MenuDefinition( "视频学习库","/maintenanceSystem/videoLibrary","el-icon-document",true,StaticPermissionsName.Page_Maintenance_Knowledges),
                   new MenuDefinition( "知识库统计","/maintenanceSystem/KnowledgeStatistics","el-icon-document",true,StaticPermissionsName.Page_Maintenance_Knowledges),
              }
          },
          new MenuDefinition( "专家坐席","","el-icon-document",true,StaticPermissionsName.Page_Maintenance_Experts)
          {
              Childs = new List<MenuDefinition>()
              {
                   new MenuDefinition( "专家管理","/maintenanceSystem/expertManagement","el-icon-document"),
                   new MenuDefinition( "求助专家","/maintenanceSystem/helpExperts","el-icon-document" ),
                   new MenuDefinition( "历史求助查询","/maintenanceSystem/queryExpert","el-icon-document" ),
                   new MenuDefinition( "待处理问题","/maintenanceSystem/pendingProblems","el-icon-document" ),
                   new MenuDefinition( "已处理问题","/maintenanceSystem/handledProblems","el-icon-document" ),
                   new MenuDefinition( "专家坐席统计","/maintenanceSystem/expertStatistics","el-icon-document" ),

              }
          },
          new MenuDefinition( "工单管理","","el-icon-document",true,StaticPermissionsName.Page_Maintenance_WorkFlow)
          {
              Childs = new List<MenuDefinition>()
              {
                     new MenuDefinition( "维修工单管理","/maintenanceSystem/maintenanceOrder/index","el-icon-document" ),
                     new MenuDefinition( "待签收维修工单","/maintenanceSystem/maintenanceMan/waitForSign","el-icon-document" ),
                     new MenuDefinition( "待维修工单","/maintenanceSystem/maintenanceMan/maintaining","el-icon-document" ),
                     new MenuDefinition( "已完成维修工单","/maintenanceSystem/maintenanceMan/maintained","el-icon-document" ),
                     new MenuDefinition( "工単统计","/maintenanceSystem/taskStatistics","el-icon-document" ),
                     new MenuDefinition( "巡检工単管理","/maintenanceSystem/patrolCreate","el-icon-document" ),
                     new MenuDefinition( "待签收工単","/maintenanceSystem/peddingSign","el-icon-document" ),
                     new MenuDefinition( "待巡检工単","/maintenanceSystem/peddingPatrol","el-icon-document" ),
                     new MenuDefinition( "已完成巡检工単","/maintenanceSystem/completedPatrol","el-icon-document" ),
                     new MenuDefinition( "巡检工単","","el-icon-document" )
                     {
                         Childs = new List<MenuDefinition>()
                         {
                                  new MenuDefinition( "巡检设备管理","/maintenanceSystem/workPatrol","el-icon-document" ),
                                  new MenuDefinition( "巡检轨迹模板","/maintenanceSystem/workTrajectory","el-icon-document" ),
                         }
                     },
              }
          },
          new MenuDefinition( "运维人员管理","","el-icon-document",true,StaticPermissionsName.Page_Maintenance_Operation)
          {
              Childs = new List<MenuDefinition>()
              {
                     new MenuDefinition( "人员基本信息","/maintenanceSystem/OperationPersonnelManagement/essentialInformation","el-icon-document" ),
                     new MenuDefinition( "人员绩效考核","/maintenanceSystem/OperationPersonnelManagement/performanceAppraisal","el-icon-document" ),
              }
          },
          new MenuDefinition( "巡检管理","","el-icon-document" )
          {
              Childs = new List<MenuDefinition>()
              {
          new MenuDefinition( "巡检任务","/maintenanceSystem/patrolTask","el-icon-document" ),
          new MenuDefinition( "模板管理","/maintenanceSystem/routingInspection/ManageTemplates","el-icon-document" ),
          new MenuDefinition( "巡检计划","/maintenanceSystem/routingInspection/ManageTemplates","el-icon-document" ),
          new MenuDefinition( "巡检记录","/maintenanceSystem/routingInspection/ManageTemplates","el-icon-document" ),

              }
          },
      }
  }
	#endregion   
      #region 系统配置管理
 
   ,  new MenuDefinition("系统配置管理","","el-icon-upload",true,StaticPermissionsName.Page_Config)
               {
                  Childs = new List<MenuDefinition>()
                   {
                       new MenuDefinition("用户管理","","el-icon-document",true,StaticPermissionsName.Page_Config_Manage)
                       {
                           Childs = new List<MenuDefinition>()
                           {
                               new MenuDefinition("用户信息列表","/configSystem/userInfo","",true,StaticPermissionsName.Page_Config_Manage_User),
                               new MenuDefinition("角色信息列表","/configSystem/roleInfo","",true,StaticPermissionsName.Page_Config_Manage_Role),
                           }
                       },
                       new MenuDefinition( "菜单管理","/configSystem/menuInfo","iconfont icon-all",true,StaticPermissionsName.Page_Config_Menu),
                       new MenuDefinition( "组织机构管理","/configSystem/organizationInfo","iconfont icon-all",true,StaticPermissionsName.Page_Config_Org),
                       //new MenuDefinition( "数据统计","","el-icon-document",true,StaticPermissionsName.Page_Config_Statistical)
                       //{
                       //    Childs = new List<MenuDefinition>()
                       //    {
                       //         new MenuDefinition( "用户登录记录","/loginList","",true,StaticPermissionsName.Page_Config_Statistical_Login),
                       //         new MenuDefinition( "用户列表","/userList","",true,StaticPermissionsName.Page_Config_Statistical_List),
                       //    }
                       //}

                   }
               }
	#endregion
           };
        }

        private  string N()
        {
            return $"Menu_Definition_{_seed++}";
        }
    }
}
