<template>
  <div class="app-home-post">
    <app-share-post v-if="isLogged"></app-share-post>
    <app-blog-card></app-blog-card>
    <app-see-more></app-see-more>
  </div>
</template>

<script>
  import SharePost from '../utilComponents/SharePost'
  import BlogCard from '../utilComponents/BlogCard'
  import SeeMore from '../utilComponents/SeeMore'

  export default {
        name: "PostCard",
        data(){
          return{
          isLogged:localStorage.token
          }
        },
        components:{
          'app-share-post':SharePost,
          'app-blog-card':BlogCard,
          'app-see-more':SeeMore
        },
    beforeMount(){


    },
        watch:{
          '$store.state.isLoginWithoutAccount':function () {
          //  this.isNotLogged = this.$store.state.isLoginWithoutAccount;
          }
        }
  }
</script>

<style scoped>

  .app-home-post{
    margin-top: 120px;
  }

</style>
