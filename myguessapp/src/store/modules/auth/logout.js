import axios from 'axios'
import {defaultState} from '../defaultState'

export const logoutMutations = {
  logout:(state)=>{
    const logoutDto = {
      userId : state.userId,
      userName : state.userName
    }

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': state.userId
    };

    axios.post('/api/auth/logout',logoutDto,{headers})
      .then(response=>{
        localStorage.removeItem("token");

        Object.assign(state,defaultState());

      }).catch(error=>{

    });

}
};

export const  logoutActions = {
  logout:({commit})=>{
    commit('logout');
  }
}
