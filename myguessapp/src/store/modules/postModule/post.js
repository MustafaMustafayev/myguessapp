import axios from 'axios';

export const postState = {
  isPostAdded:false,
  userPosts:[],
  pageNumber:1
};

export const postSetters = {
  setUserPosts:(state,userPosts)=>{
    state.userPosts = userPosts;
  }
};

export const postGetters = {
  getUserPosts:state =>{
    return state.userPosts;
  }
}

export const postMutations = {

  addPost:(state,newPost)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': state.userId
    };

    axios.post("/api/post/addPost",newPost,{headers})
      .then(response=>{
        if(response.data==201){
          state.isPostAdded = false;
        }else{
          state.isPostAdded = true;
        }
      })
      .catch(error=>{

      });
  },

  getUsersPosts:state=>{

    axios.get('/api/post/usersPosts/'+state.pageNumber)
      .then(response=>{
        console.log(response.data);
        postSetters.setUserPosts(state,response.data);

      })
      .catch(error=>{

      })
  }

};

export const postActions = {

  addPost:({commit},newPost)=>{
    commit('addPost',newPost);
  },

  getUsersPosts:({commit})=>{
    commit('getUsersPosts');

  },
};
