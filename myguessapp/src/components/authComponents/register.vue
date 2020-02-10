<template>
  <div class="register">
  <div class="container">
    <div class="row">
      <div class="col-lg-12 col-xl-9 mx-auto">
        <div class="card card-signin flex-row my-5">
          <div class="card-img-left d-none d-md-flex">
            <!-- Background image for card set in CSS! -->
          </div>
          <div class="card-body">

            <h1 class="card-title text-center">Register</h1>
            <router-link :to="{name:'Login'}" class="btn btn-sm btn-success btnLogin">Login</router-link>

            <form class="form-signin">

              <div class="form-group" :class="{'invalid': $v.userName.$error}">
                <input @input="$v.userName.$touch()" v-model="userName" type="text" placeholder="User name"  class="form-control"   autofocus>
              </div>

              <div class="form-group" :class="{'invalid': $v.name.$error}">
                <input @input="$v.name.$touch()" v-model="name" type="text" placeholder="Name"  class="form-control"   autofocus>
              </div>

            <div class="form-group" :class="{'invalid': $v.surname.$error}">
                <input @input="$v.surname.$touch()" v-model="surname" type="text" placeholder="Surname"  class="form-control"   autofocus>
              </div>

                <div class="form-group" :class="{'invalid': $v.userMail.$error}">
                 <input @input="$v.userMail.$touch()" v-model="userMail" type="email" class="form-control" placeholder="Email address" >
               </div>

               <div class="form-group" :class="{'invalid': $v.userPhone.$error}">
                 <input @input="$v.userPhone.$touch()" v-model="userPhone" type="text" class="form-control" placeholder="Phone number" >
               </div>

               <div class="form-group" :class="{'invalid': $v.userPurpose.$error}">
                 <input @input="$v.userPurpose.$touch()" v-model="userPurpose" type="text"  class="form-control" placeholder="Purpose" >
               </div>

                <div class="form-group" :class="{'invalid': $v.userPassword.$error}">
                  <input @input="$v.userPassword.$touch()" v-model="userPassword" type="password"  class="form-control" placeholder="Password" >
                </div>

                <div class="form-group" :class="{'invalid': $v.confirmPassword.$error}">
                  <input @input="$v.confirmPassword.$touch()" v-model="confirmPassword" type="password"  class="form-control" placeholder="Confirm password" >
                </div>

              <button :disabled="$v.$invalid" @click="register" class="btn btn-lg btn-success  btn-block btn-register" type="button">Register</button>
              <!--
              <a class="d-block text-center mt-2 small" href="#">Sign In</a>
              <hr class="my-4">

            <button class="btn btn-lg btn-google btn-block text-uppercase" type="submit"><i class="fab fa-google mr-2"></i> Sign up with Google</button>
            <button class="btn btn-lg btn-facebook btn-block text-uppercase" type="submit"><i class="fab fa-facebook-f mr-2"></i> Sign up with Facebook</button>
          -->
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script>

  import { required, minLength, maxLength, email, minValue, sameAs} from 'vuelidate/lib/validators'

  export default {
        name: "Register",
        data(){
          return{
            userName:'',
            name:'',
            surname:'',
            userMail:'',
            userPhone:'',
            userPurpose:'',
            userPassword:'',
            confirmPassword:''
          }
        },
    methods:{

      register(){

        const registerView = {
          'userName': this.userName,
          'name': this.name,
          'surname': this.surname,
          'userMail': this.userMail,
          'userPhone': this.userPhone,
          'userAim': this.userPurpose,
          'userPassword': this.userPassword
        };

        this.$store.dispatch('register',registerView);

        setTimeout(()=>{
          if(this.$store.state.registerSuccess){
            this.$router.push({name:'Login'});
          }else{

          }
        },500);

      }

    },
        validations:{
          userName:{
            required,
            maxLength:maxLength(50),
            minLength:minLength(5),
          },
          name:{
            required,
            maxLength:maxLength(50),
            minLength:minLength(1),
          },
          surname:{
            required,
            maxLength:maxLength(50),
            minLength:minLength(1),
          },
          userMail: {
            required,
            maxLength:maxLength(255),
            minLength:minLength(5),
            email
          },
          userPhone: {
            required,
            maxLength:maxLength(50),
            minLength:minLength(7)
          },

          userPurpose:{
            required,
            maxLength:maxLength(255),
            minLength:minLength(5)
          },
          userPassword: {
            required,
            maxLength:maxLength(255),
            minLength:minLength(5)
          },
          confirmPassword: {
            required,
            maxLength:maxLength(255),
            minLength:minLength(5),
            sameAs:sameAs('userPassword')
          }

        }
    }
</script>

<style scoped>

  @import '../../css/register.css';

  .invalid input{
   /* background-color: #dec372;*/
    box-shadow: 0px 0px 5px 3px rgba(226, 69, 69, 0.7) !important;
  }

  input{
    font-family: monospace !important;
    font-size: 16px;
  }

  .card-title{
    color: #00CC67;
    font-family: monospace;
  }

  .btn-register{
    background-color: #00CC67 !important;
    font-family: monospace;
    font-size: 20px;
    transition: box-shadow 0.5s;
  }

  .btn-register:hover{
    box-shadow: 1px 1px 11px 0px rgb(31, 80, 31);
    text-decoration: none;
  }

  .btnLogin{
    background-color: #00CC67 !important;
    font-family: monospace;
    transition: box-shadow 0.5s;
    font-size: 16px;

  }

  .btnLogin:hover{
    box-shadow: 1px 1px 11px 0px rgb(31, 80, 31);
    text-decoration: none;
  }

</style>
