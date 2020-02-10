import PostCard from '../components/utilComponents/PostCard'
import Profile from '../components/utilComponents/Profile'
import login from '../components/authComponents/login'
import Register from '../components/authComponents/Register'
import ForgotPassword from '../components/authComponents/forgotPassword'
import EditProfile from '../components/utilComponents/editProfile'


import {store} from '../store/store'
import {defaultState} from '../store/modules/defaultState'

export default[

  { path:'/', component:PostCard, name:'PostCard', beforeEnter(to, from, next){
    store.state.isRegister = false;
    next();
    } },
  { path:'/home', component:PostCard, name:'Home'},
  { path:'/profile/:id', component:Profile, name:'Profile' },
  { path:'/logout', component:login, name:'Logout', beforeEnter(to, from, next) {

      Object.assign(store.state,defaultState());
      next();

    }},
  { path:'/login', component:login, name:'Login', beforeEnter(to,from,next){
      store.state.isLogin = false;
      store.state.isLoginWithoutAccount = false;
      store.state.isRegister = false;
      store.state.registerSuccess = false;
      store.state.forgotPassword = false;
      next();
}},
  { path:'/home', component:PostCard, name:'HomeWithoutLogin', beforeEnter(to, from, next) {

    store.state.isLogin = true;
    store.state.isLoginWithoutAccount = true;
    store.state.isRegister = false;
    store.state.registerSuccess = false;
    store.state.forgotPassword = false;
    next();

    }},
  { path:'/Register', component:Register, name:'Register', beforeEnter(to, from, next) {
    store.state.forgotPassword = false;
    store.state.isRegister = true;
    next();
    }},
  { path:'/ForgotPassword', component:ForgotPassword, name:'ForgotPassword', beforeEnter(to, from, next) {

      store.state.forgotPassword = true;
      next();
    }},
  { path:'/profile/edit/:id', component: EditProfile, name:'EditProfile' }

]
