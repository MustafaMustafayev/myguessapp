<template>
    <div class="container">
      <button @click="getNextPosts" class="btn-see-more">See more...</button>
    </div>
</template>

<script>

  import axios from 'axios'

    export default {
        data(){
          return{
            pageNumber:1
          }
        },
        methods:{
          getNextPosts(){

            if(this.$router.app._route.name=="Home" || this.$router.app._route.name=="HomeWithoutLogin" || this.$router.app._route.name=="PostCard" ){
              this.$store.state.pageNumber++;
              this.pageNumber = this.$store.state.pageNumber;

              axios.get('/api/post/usersPosts/'+this.pageNumber)
                .then(response=>{

                  if(response.data.length>0) {
                    response.data.forEach(el => {
                      this.$store.state.userPosts.push(el);
                    })
                  }

                })
                .catch(error=>{
                })
            }
            else if(this.$router.app._route.name=="Profile"){
              this.$store.state.personalPageNumber++;
              this.pageNumber = this.$store.state.personalPageNumber;
              //  alert(this.$store.state.profileUserId);

              const headers = {
                'Authorization': 'Bearer '+localStorage.getItem("token"),
                'UserId': this.$store.state.userId
              };

                axios.get('/api/post/usersPostsByUserId/'+this.$route.params.id + '/'+this.pageNumber,{headers})
                .then(response=>{

                  if(response.data.length>0) {
                    response.data.forEach(el => {
                      this.$store.state.personalPosts.push(el);
                    })
                  }

                })
                .catch(error=>{

                })
            }



          }
        }
    }
</script>

<style scoped>

  .container{
    padding: 0;
  }

  .btn-see-more{
    width: 100%;
    margin-top: 10px;
    margin-bottom: 10px;
    width: 100%;
    background-color: #5cffae;
    line-height: 2;
    font-size: 16px;
    font-family: monospace;
    font-weight: 800;
    color: white;
    border-radius: 5px;
    outline: none;
    transition: background-color 0.7s;
  }

  .btn-see-more:hover{
    background-color: #00CC67;
    box-shadow: 2px 3px #adbfac;
  }

</style>
