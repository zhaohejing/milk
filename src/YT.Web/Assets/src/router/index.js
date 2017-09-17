import Vue from 'vue';
import Router from 'vue-router';
const _import = require('./_import_' + process.env.NODE_ENV);
import Full from '@/containers/Full';
/* login */
const Login = _import('login/index');
Vue.use(Router);

export const constantRouterMap = [
  { path: '/login', component: Login, hidden: true },
  {
    path: '/pages',
    redirect: '/pages/p404',
    name: 'Pages',
    component: {
      render(c) {
        return c('router-view');
      }
      // Full,
    },
    children: [
      {
        path: '404',
        name: 'Page404',
        component: _import('errorPages/Page404')
      },
      { path: '500', name: 'Page500', component: _import('errorPages/Page404') }
    ]
  }
];

export const asyncRouterMap = [
  {
    path: '/',
    redirect: '/dashboard',
    name: '首页',
    component: Full,
    hidden: false,
    children: [
      {
        path: '/dashboard',
        name: '介绍',
        icon: 'speedometer',
        component: _import('Dashboard')
      },
      // {
      //   path: '/components',
      //   name: '组件',
      //   redirect: '/components/buttons',
      //   icon: 'bookmark',
      //   component: {
      //     render(c) {
      //       return c('router-view');
      //     }
      //   },
      //   children: [
      //     {
      //       path: 'buttons',
      //       name: '按钮',
      //       icon: 'social-youtube',
      //       component: _import('components/Buttons'),
      //       hidden: false
      //     },
      //     {
      //       path: 'hoverbuttons',
      //       name: '悬停特效按钮',
      //       icon: 'wand',
      //       component: _import('components/HoverButtons')
      //     }
      //   ]
      // },
      // {
      //   path: '/charts',
      //   name: '图表',
      //   icon: 'pie-graph',
      //   component: _import('Charts')
      // },
      // {
      //   path: '/table',
      //   name: '表格',
      //   icon: 'ios-paper',
      //   component: _import('Table'),
      //   meta: { role: ['admin'] }
      // },
      // {
      //   path: '/jsontree',
      //   name: 'JSON视图',
      //   icon: 'merge',
      //   component: _import('JsonTree')
      // },
      // {
      //   path: '/tabledetail/:id',
      //   name: 'TableDetail',
      //   hidden: true,
      //   component: _import('TableDetail')
      // },
      // {
      //   path: '/tinymce',
      //   name: 'Tinymce编辑器',
      //   icon: 'android-document',
      //   component: _import('Tinymce')
      // },
      // {
      //   path: '/markdown',
      //   name: 'Markdown',
      //   icon: 'android-list',
      //   component: _import('Markdown')
      // },
      { path: '/dashboard', name: '控制台', icon: 'person' },
      {
        path: '',
        name: '客户管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'customer/client',
            name: '用户',
            icon: 'person',
            hidden: false,
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'customer/charge',
            name: '账户',
            icon: 'person-add',
            component: r => require(['views/customer/charge'], r)
          }
        ]
      },
      {
        path: '',
        name: '充值卡管理',
        icon: 'card',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'card/charge',
            name: '卡片管理',
            icon: '',
            hidden: false,
            component: r => require(['views/customer/client'], r)
          }
        ]
      },
      {
        path: '',
        name: '推广管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'generalize/user',
            name: '推广员',
            icon: 'person',
            hidden: false,
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'generalize/wechat',
            name: '群发',
            icon: 'person-add',
            component: r => require(['views/customer/client'], r)
          }
        ]
      },
      {
        path: '',
        name: '权限管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'system/role',
            name: '角色管理',
            icon: 'person',
            hidden: false,
            component: r => require(['views/manager/role'], r)
          },
          {
            path: 'system/account',
            name: '用户管理',
            icon: 'person-add',
            component: r => require(['views/manager/account'], r)
          },
          {
            path: 'system/menu',
            name: '菜单管理',
            icon: 'person-add',
            component: r => require(['views/manager/menu'], r)
          }
        ]
      },
      {
        path: '',
        name: '操作记录',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'log/audit',
            name: '日志',
            icon: 'person',
            hidden: false,
            component: r => require(['views/customer/client'], r)
          }
        ]
      },
      {
        path: '',
        name: '报表管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'statistics/a',
            name: '销售明细表',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/b',
            name: '销售汇总',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/c',
            name: '顾客取货报表',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/d',
            name: '顾客消费',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/e',
            name: '商品销售数量',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/f',
            name: '充值记录',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/g',
            name: '订单管理',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'statistics/h',
            name: '待补货记录',
            icon: 'person',
            component: r => require(['views/customer/client'], r)
          }
        ]
      }
    ]
  },
  { path: '*', redirect: '/pages/404', hidden: true }
];

const temp = constantRouterMap.concat(asyncRouterMap);
export default new Router({
  mode: 'hash',
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: temp
});
