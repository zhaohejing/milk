import Vue from 'vue';
import App from './App';
import router from './router';
import store from './store';
// import NProgress from 'nprogress'; // Progress 进度条
// import './mock/index.js'; // 该项目所有请求使用mockjs模拟
// import 'nprogress/nprogress.css'; // Progress 进度条 样式
import iView from 'iview';
import 'iview/dist/styles/iview.css';
import TreeView from 'vue-json-tree-view';
import MilkTable from 'components/table/mtable';
Vue.use(iView);
Vue.use(TreeView);
Vue.component('milk-table', MilkTable);
Vue.config.productionTip = false;
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: {
    App
  }
});
