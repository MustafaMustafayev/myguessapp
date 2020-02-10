<template>
  <div>

    <div v-for="post in this.$store.state.userPosts" class="card" style="margin-top: 20px;">
      <div class="card-body">
        <i class="fas fa-user-edit"></i>
        <router-link :to="{name:'Profile', params:{id:post.userSlug}}"><h5 class="card-title">{{post.name}} {{post.surname}}</h5></router-link>
        <div class="user-count-pointer">
        <span class="successCount"><i class="fas fa-check-circle"></i> {{post.successCount}},</span>
        <span class="failureCount"><i class="fas fa-times-circle"></i> {{post.failureCount}},</span>
        <span class="unknownCount"><i class="fas fa-question-circle"></i> {{post.unknownCount}}</span>
        </div>
        <span class="timestampContent"><i class="fas fa-clock"></i> {{post.postCreatedDate}}</span>
        <pre v-if="post.postContent.trim().length>0" class="card-text">{{post.postContent}}</pre>

        <div class="public-post-img" v-if="post.postImagePath.trim().length>0">
          <img class="img-fluid public-img" :src="post.postImagePath" alt="Card image" style="">
        </div>

     <!--   <div v-if="" class="card" style="width:100%;">
          <div class="card-body" style="width: 100%">
              <img class="card-img-top image-fluid" :src="post.postImagePath" alt="Card image" style="width:130px">
          </div>
        </div>  -->

        <div class="user-edit-panel">
          <span v-if="post.postCondition==1" class="post-current-status success">Status: success</span>
          <span v-if="post.postCondition==-1" class="post-current-status failure">Status: failure</span>
          <span v-if="post.postCondition==0" class="post-current-status unknown">Status: unknown</span>
        </div>
      </div>
    </div>

  </div>
</template>

<script>

  import {mapGetters} from 'vuex'

    export default {
        name: "BlogCard",
        data(){
          return{
          userPosts:[]
          }
        },
      computed:{
        ...mapGetters([
          'getUserPosts',
        ])
      },
      beforeMount() {

        this.$store.state.pageNumber = 1;
        this.$store.dispatch('getUsersPosts');
        this.post = '';
      }

    }
</script>

<style scoped>

  .public-post-img{
    width: 100%;
  }

  .public-img{
    object-fit: cover;

  }

  .public-img:hover{

  }

  .success{
    color: green;
    font-style: italic;
    font-size: 16px;
    font-weight: 700;
  }

  .failure{
    color: red;
    font-style: italic;
    font-size: 16px;
    font-weight: 700;
  }

  .unknown{
    font-style: italic;
    font-size: 16px;
    font-weight: 700;
    color: #0037ff;
  }

  .post-current-status{
    float: right;
  }

  .user-edit-panel{
    padding: 5px;
    height: 40px;
    width: 100%;
    height: 40px;
    background-color: rgba(249, 215, 153, 0.7);
    border-radius: 5px;
    opacity: 0.7;
    transition: background-color 1s;
    margin-top: 10px;
  }

  .user-edit-panel:hover{
    background-color: rgb(253,203,146);
    opacity: 1;
  }

  .user-count-pointer{
    display: inline-block;
    float: right;
  }

  .successCount{
    color: green;
    font-weight: 700;
  }

  .failureCount{
    color: red;
    font-weight: 700;
  }

  .unknownCount{
    color: #0037ff;
    font-weight: 700;
  }

  .timestampContent{
    color: #616770;
    font-weight: normal;
    font-size: 12px;
    display: block;
  }

  .card-title{
    background-color: #fdcb92;
    padding: 5px;
    margin: 0px;
    color: #4B4F56;
    border-radius: 5px;
    display: inline-block;
  }

  .card-text{
    padding: 5px;
    color:black;
    font-size: 18px;
    width: 100%;
    height: 100%;
    outline: none;
    border: none;
    resize: none;
    border-radius: 10px;
    margin-top: 5px;
    font-style: unset;
    font-family: monospace,unset;
    box-shadow: 0px 1px 3px 0px rgba(71, 220, 12, 1);
    transition: box-shadow 0.7s;
  }

  .card-text:hover{
    box-shadow: 1px 3px 10px 2px rgba(71, 220, 12, 0.7)
  }

  .card{
    transition: box-shadow, background-color 0.7s;
    padding: 5px;
    margin-top: 5px;
    box-shadow: 1px 2px 7px 5px rgb(197, 187, 187);
  }

  .card:hover{
    box-shadow: 0px 0px 18px 6px rgba(245, 158, 13, 0.7);
    background-color:#f7f4f4;
  }

  .fa-user-edit{
    font-size: 25px;
    color: #d2d7ff;
  }
</style>
