import axios from 'axios'
import {postSetters} from "../postModule/post";

export const personalProfileState = {
  successCount:'',
  failureCount:'',
  unknownCount:'',
  personalPosts:[],
  personalPageNumber:1,
  profileUserName:'',
  profileUserId:'',
  profileUserPhone:'',
  isUserUpdated:false
};

export const personalProfileMutations = {


  updateUser:(state,updateUserView)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': updateUserView.userId
    };

    axios.put('/api/auth/updateUser',updateUserView,{headers})
      .then(response=>{
        if(response.data=="200"){
          state.isUserUpdated = true;
        }
        else{
          state.isUserUpdated = false;
        }
      }).catch(error=>{

    });

  },

  getUserByIdForUpdate:(state,userId)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': userId
    };

    axios.get('/api/auth/userById/'+userId,{headers})
      .then(response=>{
        state.editUserData = response.data;
      }).catch(error=>{

    })
  },

  updatePost:(state,updatedPost)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': updatedPost.userId
    };

    axios.put('/api/post/updatePost',updatedPost,{headers})
      .then(response=>{

        const index = state.personalPosts.findIndex((e) => e.postId === updatedPost.postId);
        state.personalPosts[index].postContent = updatedPost.postContent;

      }).catch(error=>{
    })
  },

  changeStatusOfPersonalPost:(state,updatedPostCondition)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': updatedPostCondition.userId
    };

    axios.post('/api/post/updatePostCondition',updatedPostCondition,{headers})
      .then(response=>{

        state.successCount = response.data.successCount;
        state.failureCount = response.data.failureCount;
        state.unknownCount = response.data.unknownCount;

          state.personalPosts.forEach(post=>{
          post.successCount = response.data.successCount;
          post.failureCount = response.data.failureCount;
          post.unknownCount = response.data.unknownCount;
        })
      }).catch(error=>{

    });

  },

  deletePersonalPost:(state,deletePost)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': deletePost.userId
    };

    axios.post('/api/post/deletePost',deletePost,{headers})
      .then(response=>{
        state.successCount = response.data.successCount;
        state.failureCount = response.data.failureCount;
        state.unknownCount = response.data.unknownCount;

        state.personalPosts.forEach(post=>{
          post.successCount = response.data.successCount;
          post.failureCount = response.data.failureCount;
          post.unknownCount = response.data.unknownCount;
        });

        state.personalPosts = state.personalPosts.filter(function( obj ) {
          return obj.postId !== deletePost.postId;
        });


      }).catch(error=>{

    });

  },

  getProfileInfo : (state,id)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': state.userId
    };

    axios.get('/api/profile/profileInfo/'+id,{headers})
      .then(response=>{

        state.successCount = response.data.successCount;
        state.failureCount = response.data.failureCount;
        state.unknownCount = response.data.unknownCount;
        state.profileUserName = response.data.userName;
        state.profileUserId = response.data.userId;
        state.profileUserPhone = response.data.userPhone;
      })
      .catch(error=>{
      })

  },

  getUsersPostsByUserId:(state,id)=>{

    const headers = {
      'Authorization': 'Bearer '+localStorage.getItem("token"),
      'UserId': state.userId
    };

    axios.get('/api/post/usersPostsByUserId/'+ id + '/'+state.personalPageNumber,{headers})
      .then(response=>{

        state.personalPosts = response.data;
      })
      .catch(error=>{

      })
  }

};

export const personalProfileActions = {
  getProfileInfo:({commit},id)=>{
    commit('getProfileInfo',id);
  },

  getUsersPostsByUserId:({commit},id)=>{
    commit('getUsersPostsByUserId',id);
  },

  changeStatusOfPersonalPost:({commit},updatedPostCondition)=>{
    commit('changeStatusOfPersonalPost',updatedPostCondition);
  },

  deletePersonalPost:({commit},deletePost)=>{
    commit('deletePersonalPost',deletePost);
  },

  updatePost:({commit},updatedPost)=>{
    commit('updatePost',updatedPost);
  },

  getUserByIdForUpdate:({commit},userId)=>{
    commit('getUserByIdForUpdate',userId);
  },

  updateUser:({commit},updatedUserView)=>{
    commit('updateUser',updatedUserView);
  }
};
