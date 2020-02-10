import Vue from 'vue'
import Vuex from  'vuex'

import {loginDatas,loginGetters,loginSetters, loginMutations,loginActions} from '../store/modules/auth/login'
import {logoutMutations, logoutActions} from "./modules/auth/logout";
import {postState,postMutations,postActions, postSetters, postGetters} from "./modules/postModule/post";
import {personalProfileState, personalProfileMutations, personalProfileActions} from "./modules/profile/personalProfile";
import {registerState, registerMutations, registerActions} from "./modules/auth/register";
import {forgotPasswordState, forgotPasswordMutations, forgotPasswordActions} from "./modules/auth/forgotPassword";

Vue.use(Vuex);

const mutations = Object.assign({},logoutMutations,loginMutations,postMutations,personalProfileMutations,registerMutations,forgotPasswordMutations);
const actions = Object.assign({}, logoutActions,loginActions,postActions,personalProfileActions,registerActions,forgotPasswordActions);
const states = Object.assign({}, loginDatas,postState,personalProfileState,registerState,forgotPasswordState);
const getters = Object.assign({}, loginGetters,postGetters);
const setters = Object.assign({}, loginSetters, postSetters);



export const store = new Vuex.Store({
  state:states,
  getters:getters,
  setters:setters,
  mutations:mutations,
  actions:actions
});
