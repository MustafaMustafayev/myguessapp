<template>
  <div id="app">

    <div v-if="forgotPassword">
      <app-forgot-password></app-forgot-password>
    </div>

    <div v-if="isRegister && !forgotPassword">
      <app-register></app-register>
    </div>

    <div v-if="!this.$store.state.isLogin && !isRegister && !forgotPassword">
    <app-login></app-login>
    </div>

    <div class="container">
    <app-content v-if="this.$store.state.isLogin && !isRegister && !forgotPassword"></app-content>
    </div>

  </div>
</template>

<script>

  import login from './components/authComponents/login'
  import Content from './components/staticComponents/Content'
  import Register from './components/authComponents/register'
  import ForgotPassword from './components/authComponents/forgotPassword'

  import axios from 'axios'
  import "babel-polyfill";
  import "./app";

export default {
  name: 'app',
  data () {
    return {
      isLogin:false,
      isRegister:this.$store.state.isRegister,
      forgotPassword: this.$store.state.forgotPassword
    }
  },
  watch:{
    '$store.state.isRegister':function () {
      this.isRegister = this.$store.state.isRegister
    },
    '$store.state.forgotPassword':function () {
      this.forgotPassword = this.$store.state.forgotPassword;
      this.isRegister = this.$store.state.isRegister;
    }
  },

 async beforeMount (){

    if(localStorage.getItem("token")){

      const tokenDto = {
        tokenValue : localStorage.getItem("token")
      };
      await this.$store.dispatch("loginByToken",tokenDto);
    }


},
  methods:{
     getUserFromToken(){

    }
  },
  components:{
    'app-content':Content,
    'app-login':login,
    'app-register':Register,
    'app-forgot-password': ForgotPassword
  }
}
</script>

<style>

  #app{
    height: 100%;
  }

  body{
   background: #E9EBEE !important;
  }

  html {
  overflow-y: scroll;
  }

  body{
    height: 100%;
  }

/*body{
  background-image: url('http://bettingspies.com/wp-content/uploads/2018/09/Background-1.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  height: 100%;
  font-family: 'Numans', sans-serif;
}*/

</style>
