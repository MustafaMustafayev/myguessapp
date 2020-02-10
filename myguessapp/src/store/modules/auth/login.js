import axios from 'axios'
import {store} from "../../store";
import {defaultState} from "../defaultState";
import router from '../../../routes/router'

export const loginDatas = {
      statusCode:'',
      responseText:'',
      userId:'',
      userName:'',
      name:'',
      surname:'',
      userMail:'',
      userPhone:'',
      isLogin:localStorage.token && true,
      isLoginWithoutAccount:false,
      isRegister:false,
      forgotPassword:false,
      userSlug:''
};

export  const loginGetters = {
    getStatusCode:state =>{
      return state.statusCode;
    },
    getResponseText:state=>{
      return state.responseText;
    }
};

export const loginSetters = {
  setLoginStatus:(state,loginStatus)=>{
    state.statusCode = loginStatus.statusCode;
    state.responseText = loginStatus.responseText
  }
};

export const loginMutations ={

 loginByToken:(state,tokenDto)=>{

   return new Promise((resolve,reject)=>{
    axios.post("/api/auth/userByToken",tokenDto)
      .then(response=>{
        if(response.data.statusCode=="-3"){
          localStorage.removeItem("token");
          Object.assign(store.state,defaultState());

        }else{
          state.userId = response.data.userId;
          state.userName = response.data.userName;
          state.name = response.data.name;
          state.surname = response.data.surname;
          state.userMail = response.data.userMail;
          state.userPhone = response.data.userPhone;
          state.statusCode = response.data.statusCode,
          state.responseText = response.data.responseText;
          state.isLogin = true;
          state.userSlug = response.data.userSlug;
          state.isLoginWithoutAccount = false;
        }
      })
      .catch(error=>{

      })
      .finally(()=>{
        resolve();

      })
  })

  },

  login:(state,loginView)=>
  {
    axios.post("/api/auth/login",loginView)
      .then(response=>{
        const loginStatus = {
          statusCode:response.data.statusCode,
          responseText:response.data.responseText
        };
        loginSetters.setLoginStatus(state,loginStatus);
        if(response.data.statusCode=="1"){

          state.userId = response.data.userId;
          state.userName = response.data.userName;
          state.name = response.data.name;
          state.surname = response.data.surname;
          state.userMail = response.data.userMail;
          state.userPhone = response.data.userPhone;
          state.isLogin = true;
          state.isLoginWithoutAccount = false;
        }
      }).catch(error=>{
      alert('Error occured');
    })
  }
};

export const loginActions = {

  login:({commit},loginView)=>{
    commit('login',loginView);
  },
  loginByToken:({commit},tokenDto)=>{
    commit('loginByToken',tokenDto);
  },
};
