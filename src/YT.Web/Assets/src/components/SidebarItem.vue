<template>
  <div class="sidebar">
    <nav class="sidebar-nav">
      <ul class="nav">
        <template v-for="item in routes">
          <router-link :key="item.url" tag="li" class="nav-item nav-dropdown" 
          v-if="item.children&&item.children.length>0" :to="item.children[0].path" disabled>
            <div class="nav-link nav-dropdown-toggle" @click="handleClick">
              <Icon :type="item.icon" />{{ item.name}} </div>
            <ul class="nav-dropdown-items">
              <li class="nav-item" :key="child.url" v-for="child in item.children" v-if='!child.hidden' @click="addActive">
                <router-link :to="child.children[0].path" class="nav-link" v-if="child.children">
                  <Icon :type="child.icon" />{{ child.name}} </router-link>
                <router-link :to="item.path+'/'+child.path" class="nav-link" v-else>
                  <Icon :type="child.icon" /> {{ child.name}} </router-link>
              </li>
            </ul>
          </router-link>
          <li class="nav-item" :key="item.url" v-if="!item.children">
            <router-link :to="item.path" class="nav-link" exact>
              <Icon :type="item.icon" />{{ item.name}} </router-link>
          </li>
        </template>
      </ul>
    </nav>
  </div>
</template>

<script>

export default {
  name: 'SidebarItem',
  props: {
    routes: {
      type: Array
    }
  },
  methods: {
    handleClick(e) {
      e.preventDefault()
      e.target.parentElement.classList.toggle('open')
    },
    addActive(e) {
      e.preventDefault()
      e.target.parentElement.parentElement.parentElement.classList.add('open')
    }
  },
  mounted() {
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.wscn-icon {
  margin-right: 10px;
}

.hideSidebar .menu-indent {
  display: block;
  text-indent: 10px;
}
</style>

