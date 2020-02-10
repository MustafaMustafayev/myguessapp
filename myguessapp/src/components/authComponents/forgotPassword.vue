<template>
  <div>
  <div class="login-container">
    <div class="d-flex justify-content-center h-100">
      <div class="card">
        <div class="card-header">
          <h3 style="display: inline-block;">Guess app</h3>
          <router-link :to="{name:'Login'}"  class="btn btn-sm btn-warning btnGoToLogin">Login</router-link>
        </div>
        <div class="card-body">
          <form>

            <div class="input-group form-group" :class="{'invalid': $v.userName.$error}">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-user"></i></span>
              </div>
              <input type="text" @input="$v.userName.$touch()" v-model="userName"  class="form-control" placeholder="username">
            </div>

            <div class="input-group form-group" :class="{'invalid': $v.userMail.$error}">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-envelope-square"></i></span>
              </div>
              <input type="email" @input="$v.userMail.$touch()" v-model="userMail"  class="form-control" placeholder="email address">
            </div>

            <div class="form-group">
              <span class="loginErrorMessage" v-if="errorMessage">{{responseText}}</span>
              <input type="button" @click="sendRequestForCheckingUser" value="Send" class="btn float-right login_btn">
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>

          <div class="modal fade" id="modalUpdatePasswordCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title"  id="postModalLongTitle">Update password</h5>
                  <button type="button" class="close" style="text-align: center" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div class="form-group" :class="{'invalid': $v.userPassword.$error}">
                    <input @input="$v.userPassword.$touch()" v-model="userPassword" type="password"  class="form-control" placeholder="Password" >
                  </div>

                  <div class="form-group" :class="{'invalid': $v.confirmPassword.$error}">
                    <input @input="$v.confirmPassword.$touch()" v-model="confirmPassword" type="password"  class="form-control" placeholder="Confirm password" >
                  </div>
                </div>
                <div class="modal-footer">
                  <button type="button" @click="closeUpdatePassword" class="btn btn-secondary" data-dismiss="modal">Close</button>
                  <button type="button" @click="updatePassword" class="btn btn-success">Edit</button>
                </div>
              </div>
            </div>
          </div>
  </div>

</template>

<script>

  import { required, minLength, maxLength, email, minValue, sameAs} from 'vuelidate/lib/validators'

  export default {
        name: "ForgotPassword",
        data(){
          return{
            userName:'',
            userMail:'',
            errorMessage:false,
            responseText:'',
            userPassword:'',
            confirmPassword:''
          }
        },
        validations:{
          userName:{
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
        },
        methods:{

          closeUpdatePassword(){
            this.userPassword = '';
            this.confirmPassword = '';
            $('#modalUpdatePasswordCenter').modal('hide');
          },

          updatePassword(){

            const updatedUserPassword = {
              userId: this.$store.state.forgotPasswordUserId,
              updatedPassword: this.userPassword
            };

            this.$store.dispatch('updatePassword',updatedUserPassword);

            setTimeout(()=>{
              this.errorMessage = true;
              if(this.$store.state.isPasswordUpdated){
                this.responseText = "Password updated";
              }else{
                this.responseText = "Sorry, now we can not update your password!";
              }

              this.userPassword = '';
              this.confirmPassword = '';
              $('#modalUpdatePasswordCenter').modal('hide');

            },700);

          },

          sendRequestForCheckingUser(){
            const forgotPasswordView = {
              userName: this.userName,
              userMail: this.userMail
            };

            this.$store.dispatch('forgotPassword',forgotPasswordView);

            setTimeout(()=>{
              if(this.$store.state.forgotPasswordUserId>0){
                this.errorMessage = false;
                $('#modalUpdatePasswordCenter').modal('show');

              }else{
                this.errorMessage = true;
                this.responseText = "Invalid data!";
              }
            },500);

          }
        }

    }
</script>

<style scoped>

  .invalid{
    box-shadow: 0px 0px 5px 3px rgba(226, 69, 69, 0.7) !important;
  }
  
  input{
    font-family: monospace;
    font-size: 16px;
    border-radius:0px;
  }

  .btnGoToLogin
  {
    float: right; color: black; outline:none
  }

  .btnGoToLogin:hover{
    color: black;
    background-color: white;
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
    height: 270px;
    margin-top: auto;
    margin-bottom: auto;
    width: 400px;
    background-color: rgba(0,0,0,0.5) !important;
  }

  .loginErrorMessage{
    color: yellow;
    font-family: monospace;
    font-size: 16px;
  }

  .social_icon span{
    font-size: 60px;
    margin-left: 10px;
    color: #FFC312;
  }

  .modal-title{
    color: #00CC67;
    font-family: monospace;
    font-size: 20px;
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

  .social_icon{
    position: absolute;
    right: 20px;
    top: -45px;
  }

  .input-group-prepend span{
    width: 50px;
    background-color: #FFC312;
    color: black;
    border:0 !important;
  }

  input:focus{
    outline: 0 0 0 0  !important;
    box-shadow: 0 0 0 0 !important;

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
