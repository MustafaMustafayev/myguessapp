import axios from 'axios';

export const registerState = {

  registerSuccess:false

};

export const registerMutations = {

  register:(state,registerView)=>{

    axios.post('/api/auth/register',registerView)
      .then(response=>{
        if(response.data=="200"){
          state.registerSuccess = true;
        }
      }).catch(error=>{

    })


  }

};

export const registerActions = {
  register:({commit},registerView)=>{
    commit('register',registerView);
  },
};
