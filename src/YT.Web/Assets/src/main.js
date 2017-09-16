import Vue from 'vue';
import App from './App';
import router from './router';
import store from './store';
import dtime from 'time-formater';
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
/* 格式化日期*/
Vue.prototype.$fmtTime = (date, format) => {
  return dtime(date).format(format || 'YYYY-MM-DD HH:mm:ss');
};
/* 列表格式转换成树格式
 * @param data 数组
 * @param parentId 父节点id
 * @param pidField 父节点字段名
 */
const converToTreedata = (data, parentId, pidField, grants) => {
  const list = [];
  data.forEach(item => {
    if (item[pidField] === parentId) {
      item.children = converToTreedata(data, item.id, pidField, grants);
      item.title = item.displayName;
      if (grants) {
        const has = grants.findIndex(key => key === item.name) >= 0
        item.checked = has;
        item.expand = has;
      }
      list.push(item);
    }
  });
  return list;
};
Vue.prototype.$converToTreedata = converToTreedata;
Vue.config.productionTip = false;
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  data: {
    eventHub: new Vue()
  },
  components: {
    App
  }
});