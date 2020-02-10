
import axios from 'axios';

export const forgotPasswordState = {

forgotPasswordUserId:'',
isPasswordUpdated:false

};


export const forgotPasswordMutations = {

  forgotPassword:(state, forgotPasswordView)=>{
    axios.post('/api/auth/forgotPassword', forgotPasswordView)
      .then(response=>{
        state.forgotPasswordUserId = response.data;
      })
      .catch(error=>{

      });
  },

  updatePassword: (state, updatedPassword)=>{
    axios.put('/api/auth/updatePassword', updatedPassword)
      .then(response=>{

        if(response.data=="200"){
          state.isPasswordUpdated = true;
        }else{
          state.isPasswordUpdated = false;
        }
      })
      .catch(error=>{

      });
  }
};

export const forgotPasswordActions = {
  forgotPassword:({commit},forgotPasswordView)=>{
    commit('forgotPassword',forgotPasswordView);
  },
  updatePassword:({commit},updatedPassword)=>{
    commit('updatePassword',updatedPassword);
  }
};
