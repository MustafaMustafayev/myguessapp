<template>
  <nav class="navbar navbar-expand-lg fixed-top navbar-light fluid main-nav-bar">
    <a class="navbar-brand" href="#">
      <router-link exact="" class="nav-link" :to="{name:'PostCard'}"><i class="far fa-futbol"></i></router-link>
    </a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <form class="form-inline my-2 my-lg-0">

        <input v-model="searchValue" class="form-control mr-sm-2 searchInput" id="searchInput" type="search" placeholder="Search" aria-label="Search">
        <div class="form-control mr-sm-2 search-result" v-if="show">
           <ul>
             <li v-for="search in searchResult"  v-on:click="show=false" ><router-link class="search-link" :to="{name:'Profile',params:{id:search.userSlug}}">{{search.name}} {{search.surname}}</router-link></li>
           </ul>
        </div>
       <!-- <button class="btn btn-outline-success my-2 my-sm-0" style="margin-right: 5px" type="submit">
          <i class="fas fa-search"></i>
        </button>-->
      </form>
      <ul class="navbar-nav mr-auto auth-nav">
        <li class="nav-item active" v-if="!this.$store.state.isLoginWithoutAccount">
          <router-link exact="" class="nav-link" :to="{name:'Home'}">Home</router-link>
        </li>
        <li class="nav-item" v-if="!this.$store.state.isLoginWithoutAccount">
          <router-link exact="" class="nav-link" :to="{name:'Profile', params:{id:this.$store.state.userSlug}}">Profile</router-link>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fas fa-user-cog"></i>
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <router-link :to="{name:'Login'}" v-if="this.$store.state.isLoginWithoutAccount" class="dropdown-item">Log in</router-link>
            <router-link :to="{name:'Register'}" v-if="this.$store.state.isLoginWithoutAccount" class="dropdown-item">Register</router-link>
            <div class="dropdown-divider"></div>
            <button v-if="!this.$store.state.isLoginWithoutAccount" @click="logOutAction" class="dropdown-item">Log out</button>
          </div>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>

  import {mapActions} from 'vuex';
  import axios from 'axios';

    export default {
        data(){
          return{
          show: false,
          searchValue: '',
          searchResult: []
          }
        },
      watch:{
          'searchValue':function () {
            if(this.searchValue.length>=3){
              this.searchUsers();
              this.show = true;
            }else{
              this.show = false;
            }
          }
      },
      beforeMount(){
        this.searchValue = '';
        this.searchResult = [];
      },
        computed:{

        },
        methods:{
          ...mapActions([
            'logout',
          ]),

          searchUsers(){

            const headers = {
              'Authorization': 'Bearer '+localStorage.getItem("token")
            };

            axios.get('/api/profile/usersList/'+this.searchValue,{headers})
              .then(response=>{
                this.searchResult = response.data;
              }).catch(error=>{

            })
          },

          logOutAction(){
            this.$store.dispatch('logout');
            this.$router.push({name:'Logout'});
          }
        }
    }
</script>

<style scoped>

  .auth-nav{
    margin-right: 115px !important;
    flex: 1;
    justify-content: flex-end;
  }

  .searchInput{
    width: 250px;
    font-family: monospace;
    font-size: 16px;
  }

  .search-result{
    position: absolute;
    margin-top: 170px;
    width: 250px;
    height: 300px;
    background-color: white;
    box-shadow: 1px 1px 2px 3px rgba(179, 157, 157, 0.7);
  }


  .search-link{
    font-family: monospace;
    font-size: 16px;
    text-align: inherit;
    float: left;
    transition: background-color 0.5s;
  }

  .search-link:hover{
    background-color: antiquewhite;
  }

  ul{
    padding: 0px;
  }

  li{
    list-style-type: none;
    text-decoration: none;
  }

  li a{
    text-decoration: none;
    color: #222;
    width: 100%;
  }

  .navbar{

    margin-bottom: 15px;
    background-color: #F58634;
  }
  
  .fa-futbol{
    font-size: 40px;
  }

  .navbar-brand{
    padding: 10px;
  }

  .navbar-toggler{
    outline: none;
    background-color: #00cc67;
  }

  .nav-link:hover{
    box-shadow: 1px 1px 2px 2px rgb(226, 226, 177);
  }

  .nav-link{
    transition: box-shadow 0.7s;
    font-size: 20px;
    color: white !important;
  }

  .dropdown-menu:hover{
    box-shadow: 1px 2px darkgrey !important;
    background-color:blanchedalmond;
  }

  .dropdown-menu{
    transition: box-shadow, background-color 0.7s;
    box-shadow: 0.5px 0.5px darkgray !important;
  }

</style>
