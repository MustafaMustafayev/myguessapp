<template>
  <div class="personal-content">
  <app-share-post v-if="isPersonalProfile"></app-share-post>
  <app-personal-post></app-personal-post>
    <app-see-more v-if="this.$store.state.personalPosts.length>0"></app-see-more>
  </div>
</template>

<script>
  import SharePost from '../utilComponents/SharePost'
  import PersonalBlogCard from '../utilComponents/PersonalBlogCard'
  import SeeMore from '../utilComponents/SeeMore'

    export default {
        data(){
          return{
          isPersonalProfile:this.$route.params.id == this.$store.state.userSlug
          }
        },
      components:{
        'app-share-post':SharePost,
        'app-personal-post':PersonalBlogCard,
        'app-see-more':SeeMore
      },
      watch:{
          '$route.params.id':function () {
            this.isPersonalProfile = this.$route.params.id == this.$store.state.userSlug
          },
          '$store.state.userId':function () {
            this.isPersonalProfile = this.$route.params.id == this.$store.state.userSlug
          }
      }
    }
</script>

<style scoped>

  .personal-content{
    width:100%;
  }

</style>
