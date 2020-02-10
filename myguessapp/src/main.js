import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import router from './routes/router'
import Vuelidate from 'vuelidate'
import {store} from './store/store'
import axios from 'axios'

axios.defaults.baseURL = "https://localhost:44336";


Vue.use(VueRouter);

Vue.use(Vuelidate);

const routes = new VueRouter({
  routes:router,
  mode:'history'
});


new Vue({
  el: '#app',
  render: h => h(App),
  router:routes,
  store:store
})
