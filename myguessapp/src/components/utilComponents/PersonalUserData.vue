<template>
    <div class="personal-left-side-bar">
      <i class="fas fa-user-circle"></i>

      <p class="user-info username"></p>
      <p class="user-info">{{this.$store.state.profileUserName}}</p>
      <p class="user-info" style="margin-bottom: 1px;">{{this.$store.state.profileUserPhone}}</p>

      <router-link v-if="isLoggedUser" :to="{name:'EditProfile', params:{id:this.$store.state.userSlug}}" class="btn btn-success btnUserEdit">Edit profile</router-link>

      <p class="user-info success"><i class="fas fa-check-circle"></i> {{this.$store.state.successCount}}</p>
      <p class="user-info failure"><i class="fas fa-times-circle"></i> {{this.$store.state.failureCount}}</p>
      <p class="user-info unknown"><i class="fas fa-question-circle"></i> {{this.$store.state.unknownCount}}</p>
    </div>
</template>

<script>

    export default {
        data(){
          return{
            userId:'',
            isLoggedUser:this.$store.state.userSlug == this.$route.params.id
          }
        },

      beforeMount() {
          this.$store.dispatch('getProfileInfo', this.$route.params.id);
      },
      watch:{
          '$route.params.id':function(){
            this.$store.dispatch('getProfileInfo', this.$route.params.id);
            this.isLoggedUser = this.$store.state.userSlug == this.$route.params.id;
          },
          '$store.state.userId':function () {
            this.$store.dispatch('getProfileInfo', this.$route.params.id);
          //  this.userId = this.$store.state.userId;
            this.isLoggedUser = this.$store.state.userSlug == this.$route.params.id;
          }
      }
    }
</script>

<style scoped>

  .btnUserEdit{
    font-family: monospace;
    outline: none;
    padding: 2px;
    background-color: #00CC67 !important;
    box-shadow: 1px 1px 1px 1px rgb(22, 101, 22);
    transition: box-shadow 0.7s;
    margin-bottom: 5px;
  }

  .btnUserEdit:hover{
    box-shadow: 1px 1px 11px 0px rgb(31, 80, 31);
  }

  .personal-left-side-bar{
    background-color: white;
    padding: 10px;
    border-radius: 5px;
    height: max-content;
    margin-top: 120px;
    margin-left: 10px;
    overflow:hidden;
    width: 195px; /* Set the width of the sidebar */
    position: fixed; /* Fixed Sidebar (stay in place on scroll) */
    z-index: 1; /* Stay on top */
    top: 0; /* Stay at the top */
    left: 0;
    overflow-x: hidden; /* Disable horizontal scroll */
    padding-top: 20px;
    height: 100%;
    box-shadow: 0px 0px 10px 1px rgba(245,134,52,0.7);
    transition: box-shadow 0.7s;
  }

  .personal-left-side-bar:hover{
    box-shadow: 0px 0px 10px 5px rgba(245,134,52,0.7);
  }

  .success{
    color: #00CC67 !important;
  }

  .failure{
    color: red !important;
  }

  .unknown{
    color: darkgoldenrod !important;
  }

  .user-info{
    color: #385898;
    margin:0px;
    font-weight: bold;
    font-family: monospace !important;
    font-size: 18px;
  }

  .username{
    font-size: 20px; font-weight: 700
  }

  .fa-user-circle{
    font-size: 45px;
    color: cornflowerblue;
    font-style: inherit;
  }

</style>
