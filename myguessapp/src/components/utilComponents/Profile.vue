<template>
<div style="display: flex; margin-top: 120px">
  <app-personal-side-bar class="personal-left-side-bar"></app-personal-side-bar>
  <app-personal-content class="personal-content"></app-personal-content>
</div>
</template>

<script>

  import PersonalContent from '../utilComponents/PersonalContent'
  import PersonalUserData from './PersonalUserData'

    export default {
        name: "Profile",
        components:{
          'app-personal-content':PersonalContent,
          'app-personal-side-bar':PersonalUserData
        }

    }
</script>

<style scoped>


  .personal-content{
    margin-left: 180px; /* Same as the width of the sidebar */
    padding: 0px 10px;
  }


</style>
