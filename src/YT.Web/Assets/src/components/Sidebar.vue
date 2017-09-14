<template>
  <div class="sidebar">
    <nav class="sidebar-nav">
      <sidebar-item :routes='menus'></sidebar-item>

    </nav>
  </div>
</template>
<script>
import SidebarItem from './SidebarItem';
import { mapGetters, mapActions } from 'vuex';
import { userMenus } from 'api/menu';
export default {
  name: 'sidebar',
  data() {
    return {
      menus: null
    }
  },
  created: function() {
    this.getUserMenus();
  },
  components: { SidebarItem },
  computed: {
    ...mapGetters([
      'siderbar_routers'
    ]),
  },
  methods: {
    handleClick(e) {
      e.preventDefault()
      e.target.parentElement.classList.toggle('open')
    },
    getUserMenus() {
      this.menus = [
        {path:'/dashboard',name:'控制台',icon:'person' },
        {
          path: '', name: '客户管理', icon: 'person-stalker',
          children: [
            {
              path: 'customer/user',
              name: '用户',
              icon: 'person',
              hidden: false
            },
            {
              path: 'customer/account',
              name: '账户',
              icon: 'person-add'
            }
          ]
        },
        {
          path: '', name: '充值卡管理', icon: 'card',
          children: [
            {
              path: 'card/charge',
              name: '卡片管理',
              icon: '',
              hidden: false
            }
          ]
        },
        {
          path: '', name: '推广管理', icon: 'person-stalker',
          children: [
            {
              path: 'generalize/user',
              name: '退管员',
              icon: 'person',
              hidden: false
            },
            {
              path: 'generalize/wechat',
              name: '群发',
              icon: 'person-add'
            }
          ]
        },
         {
          path: '', name: '权限管理', icon: 'person-stalker',
          children: [
            {
              path: 'system/role',
              name: '角色管理',
              icon: 'person',
              hidden: false
            },
            {
              path: 'system/account',
              name: '用户管理',
              icon: 'person-add'
            } , {
            path: 'system/menu',
            name: '菜单管理',
            icon: 'person-add'
          }
          ]
        },
         {
          path: '', name: '操作记录', icon: 'person-stalker',
          children: [
            {
              path: 'log/audit',
              name: '日志',
              icon: 'person',
              hidden: false
            }
          ]
        },
          {
          path: '', name: '报表管理', icon: 'person-stalker',
          children: [
            {
              path: 'statics/a',
              name: '销售明细表',
              icon: 'person'
            },
             {
              path: 'statics/b',
              name: '销售汇总',
              icon: 'person'
            },
             {
              path: 'statics/c',
              name: '顾客取货报表',
              icon: 'person'
            },
              {
              path: 'statics/d',
              name: '顾客消费',
              icon: 'person'
            },
              {
              path: 'statics/e',
              name: '商品销售数量',
              icon: 'person'
            },
              {
              path: 'statics/f',
              name: '充值记录',
              icon: 'person'
            },
               {
              path: 'statics/g',
              name: '订单管理',
              icon: 'person'
            },
               {
              path: 'statics/f',
              name: '待补货记录',
              icon: 'person'
            },
          ]
        },
      ]
    }
  }
}
</script>

<style lang="css">
.nav-link {
  cursor: pointer;
}
</style>
