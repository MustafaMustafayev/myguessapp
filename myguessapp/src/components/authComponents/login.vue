<template>
  <div class="login-container">
  <div class="d-flex justify-content-center h-100">
    <div class="card">
      <div class="card-header">
        <h3>Guess app</h3>
      </div>
      <div class="card-body">
        <form>
          <div class="input-group form-group" :class="{'invalid': $v.userName.$error}">
            <div class="input-group-prepend">
            <span class="input-group-text"><i class="fas fa-user"></i></span>
          </div>
            <input type="text" v-model="userName" @input="$v.userName.$touch()" class="form-control" placeholder="username">

          </div>
          <div class="input-group form-group" :class="{'invalid': $v.userPassword.$error}">
            <div class="input-group-prepend">
              <span class="input-group-text"><i class="fas fa-key"></i></span>
            </div>
            <input type="password" v-model="userPassword" @input="$v.userPassword.$touch()" class="form-control" placeholder="password">
          </div>
          <div class="form-group">
            <span class="loginErrorMessage" v-if="errorMessage">{{responseText}}</span>
            <input type="button" @click="loginAction" :disabled="$v.$invalid" value="Login" class="btn float-right login_btn">
          </div>
        </form>
      </div>
      <div class="card-footer">
        <div class="d-flex justify-content-center links">
          Don't have an account?<router-link :to="{name:'Register'}">Create account</router-link>
        </div>
        <div class="d-flex justify-content-center">
          <router-link :to="{name:'ForgotPassword'}">Forgot your password?</router-link>
        </div>
        <div class="d-flex justify-content-center">
         <!-- <router-link exact="" :to="{name:'HomeWithoutLogin'}">Join without login</router-link> -->
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script>

  import axios from 'axios'
  import {mapGetters} from 'vuex'
  import {mapActions} from 'vuex'
  import { required, minLength, email, minValue, sameAs} from 'vuelidate/lib/validators'

    export default {
        data(){
          return{
            userName:'',
            userPassword:'',
            statusCode:'',
            responseText:'',
            errorMessage:true
          }
        },
      validations:{
        userName:{
          required,
          minLength:minLength(5)
        },
        userPassword:{
          required,
          minLength:minLength(5)
        }
      },
        computed:{
          ...mapGetters([
            'getStatusCode',
            'getResponseText'
          ])
        },
      methods: {
        ...mapActions([
          'login',
        ]),

        loginAction(){

          const loginView = {
            userName:this.userName,
            userPassword:this.userPassword,
          };

          axios.post("/api/auth/login",loginView)
            .then(response=>{

              if(response.data.statusCode!="1") {
                this.errorMessage = true;
                this.responseText = response.data.responseText;
              }
              else{
                this.$store.state.userId = response.data.userId;
                this.$store.state.userName = response.data.userName;
                this.$store.state.name = response.data.name;
                this.$store.state.surname = response.data.surname;
                this.$store.state.userMail = response.data.userMail;
                this.$store.state.userPhone = response.data.userPhone;
                this.$store.state.statusCode = response.data.statusCode,
                this.$store.state.responseText = response.data.responseText;
                this.$store.state.isLogin = true;
                this.$store.state.isLoginWithoutAccount = false;
                this.$store.state.userSlug = response.data.userSlug;
                this.errorMessage = false;
                localStorage.setItem("token",response.data.token);
                this.$router.push({name: 'Home'});

              }
            })
            .catch(error=>{

            });
      }
    }}
</script>

<style scoped>

  .loginErrorMessage{
    color: yellow;
  }

  .invalid input{
  /*  background-color: rgba(255, 0, 0, 0.7); */
    box-shadow: 0px 0px 5px 3px rgba(226, 69, 69, 0.7) !important;
  }

  input{
    font-family: monospace;
    font-size: 16px;
  }

  .login-container{
   position: absolute;
    width: 100%;
    height: 100%;
    align-content: center;
    position: fixed;
    top: 49%;
    left: 49%;
    transform: translate(-49%, -49%);
    background-image: url('http://bettingspies.com/wp-content/uploads/2018/09/Background-1.jpg');
    background-size: cover;
    background-repeat: no-repeat;
    height: 100%;
    font-family: 'Numans', sans-serif;
  }

  .card{
    height: 370px;
    margin-top: auto;
    margin-bottom: auto;
    width: 400px;
    background-color: rgba(0,0,0,0.5) !important;
  }

  .social_icon span{
    font-size: 60px;
    margin-left: 10px;
    color: #FFC312;
  }

  .card-body{
    flex: none;
  }

  .social_icon span:hover{
    color: white;
    cursor: pointer;
  }

  .card-header h3{
    color: white;
  }


  .input-group-prepend span{
    width: 50px;
    background-color: #FFC312;
    color: black;
    border:0 !important;
  }

  input{
    margin-left: 2px;
  }

  input:focus{
    outline: 0 0 0 0  !important;
    box-shadow: none;
  }

  .remember{
    color: white;
  }

  .remember input
  {
    width: 20px;
    height: 20px;
    margin-left: 15px;
    margin-right: 5px;
  }

  .login_btn{
    color: black;
    background-color: #FFC312;
    width: 100px;
  }

  .login_btn:hover{
    color: black;
    background-color: white;
  }

  .links{
    color: white;
  }

  .links a{
    margin-left: 4px;
  }

  a{
    color: #ffe000;
    text-decoration: none;
  }
</style>
