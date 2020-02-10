<template>
  <div>
    <app-nav-bar></app-nav-bar>
    <router-view></router-view>
  </div>
</template>

<script>

  import NavBar from './NavBar'

    export default {
        name: "Content",
         components:{
        'app-nav-bar':NavBar
      },
      created() {
        this.$store.dispatch('getUsersPosts');
      }
    }
</script>

<style scoped>

</style>
