<template>
    <div style="" class="share-post">
      <div class="card">
        <div class="card-body">
          <h5 class="card-title">
            <i class="fas fa-user-edit"></i>
            Create guess</h5>
          <form enctype="multipart/form-data">
          <div :class="{'invalid': $v.post.$error}">
          <textarea id="postArea" v-model="post" @input="$v.post.$touch()" placeholder="What's on your mind?" class="card-text"></textarea>

         <div class="image-container">
          <div class="post-image-div">
            <input  type="file"  id="file" ref="file" class="inputfile inputfile-4" v-on:change="handleFileUpload"/>
            <label style="height: 100%" class="label-inputfile" for="file"><figure><svg xmlns="http://www.w3.org/2000/svg" width="20" height="17" viewBox="0 0 20 17"><path d="M10 0l-5.2 4.9h3.3v5.1h3.8v-5.1h3.3l-5.2-4.9zm9.3 11.5l-3.2-2.1h-2l3.4 2.6h-3.5c-.1 0-.2.1-.2.1l-.8 2.3h-6l-.8-2.2c-.1-.1-.1-.2-.2-.2h-3.6l3.4-2.6h-2l-3.2 2.1c-.4.3-.7 1-.6 1.5l.6 3.1c.1.5.7.9 1.2.9h16.3c.6 0 1.1-.4 1.3-.9l.6-3.1c.1-.5-.2-1.2-.7-1.5z"/></svg></figure></label>
          </div>
            <div class="post-image-show" v-if="showPostImagePreview" >
              <img :src="postImgUrl" class="post-image-src" id="imgPost">
            </div>
         </div>

          <span v-if="this.$store.state.isPostAdded" class="errorPost">ERROR</span>
          </div>
            <div>
             <button @click.prevent="addPostAction"  class="btn-post"><span class="btn-post-span">Post</span></button>
            </div>
          </form>
        </div>
      </div>
    </div>
</template>

<script>

  import { required, minLength, email, minValue, sameAs} from 'vuelidate/lib/validators'
  import axios from 'axios'

  export default {
        data(){
          return{
          post:'',
          showSharePost:false,
          file:'',
          fileUploaded:0,
          filePath:'',
          postImgUrl:'',
          showPostImagePreview:false
          }
        },
    validations:{
          post:{
            /* required,
            minLength:minLength(5) */
          }
    },
    beforeMount(){

    },
    watch:{
      'filePath':function () {
        if(this.fileUploaded==1){
          this.addPost();
        }
      }
    },
        methods:{

          addPost(){

            const newPost = {
              postContent : this.post,
              userId : this.$store.state.userId,
              PostImagePath: this.filePath
            };

            this.$store.dispatch('addPost',newPost);
            this.post = '';
            setTimeout(()=>{
              if(this.$router.app._route.name=="Home" || this.$router.app._route.name=="PostCard"){

                this.$store.state.pageNumber = 1;
                this.$store.dispatch('getUsersPosts',newPost);
                this.fileUploaded = -1;
              }
              else if(this.$router.app._route.name=="Profile"){

                this.$store.state.personalPageNumber = 1;
                this.$store.dispatch('getUsersPostsByUserId', this.$store.state.userSlug);
                this.$store.dispatch('getProfileInfo', this.$route.params.id);
              }
            },1000);

            this.$refs.file.value = '';
            this.showPostImagePreview = false;

          },

          handleFileUpload(e){
            this.file = this.$refs.file.files[0];
            const file = e.target.files[0];
            this.postImgUrl = URL.createObjectURL(file);
            this.showPostImagePreview = true;
          },

          addPostAction(){

            if(this.post.trim().length==0 && this.file==''){
              return;
            }

            // IMAGE UPLOAD
            let formData = new FormData();
            formData.append('file', this.file);
            formData.append('userName', 'MustafaX');

            if(this.file!=''){

              const headers = {
                'Authorization': 'Bearer '+localStorage.getItem("token"),
                'UserId': this.$store.state.userId,
                'Content-Type': 'multipart/form-data'
              };

              axios.post( '/api/post/addPostImage',formData,{headers}
              ).then(response=> {
                this.filePath =response.data;
                this.fileUploaded = 1;
                this.file = '';
              })
                .catch(function(){
                  console.log('FAILURE!!');
                });
            }
            else if(this.file == ''){

                  this.filePath = '';
                  this.addPost();
                  this.fileUploaded = -1;
                  this.file = '';
            }

          /*  const headers = {
              'Authorization': 'Bearer '+localStorage.getItem("token"),
              'UserId': this.$store.state.userId,
              'Content-Type': 'multipart/form-data'
            };

            const newPost = {
              postContent : this.post,
              userId : this.$store.state.userId
            };

            axios.post( '/api/post/addPostImage',formData,{headers}
            ).then(function(){
              console.log('SUCCESS!!');
            })
              .catch(function(){
                console.log('FAILURE!!');
              });
*/
            /*  ADD POST
            const newPost = {
              postContent : this.post,
              userId : this.$store.state.userId
            };

             this.$store.dispatch('addPost',newPost);
             this.post = '';
             setTimeout(()=>{
              if(this.$router.app._route.name=="Home"){

                this.$store.state.pageNumber = 1;
                this.$store.dispatch('getUsersPosts',newPost);

               }
               else if(this.$router.app._route.name=="Profile"){

                this.$store.state.personalPageNumber = 1;
                this.$store.dispatch('getUsersPostsByUserId', this.$store.state.userSlug);
                this.$store.dispatch('getProfileInfo', this.$route.params.id);
              }
             },500); */
          }
        }
    }
</script>

<style scoped>

  @import "../../css/custom.css";

  .image-container{
    text-align: center;
    display: flex;
  }

  .post-image-show{
    width:  130px;
    display: inline-block;
    height: 130px;
    border: 2px solid rgb(245,134,52);
    border-radius: 5px;
    float: left;
    transition: box-shadow 0.7s;
  }

  .post-image-src{
    width: 100%;
    height: 100%;
    border-radius: 2px;
  }

  .post-image-div{
    width: 25%;
    display: inline-block;
    height: 130px;
    float: left;
  }

  .post-image-show:hover{
    box-shadow: 0px 0px 12px 4px rgba(245, 158, 13, 0.7);
  }

  .label-inputfile{
    height: 100%;
  }

  .inputfile{
    display: none;
  }

  .share-post{
    transition: box-shadow 0.5s;
  }

  .share-post:hover{
    box-shadow: 0px 0px 18px 8px rgba(245, 158, 13, 0.7);
  }

  .errorPost{
    color: red;
  }

  .card{
    box-shadow: 1px 2px 7px 5px rgb(197, 187, 187);
    padding: 5px;
  }

  .card-body{
    padding: 0px;
  }

  .card-title{
    background-color: #fdcb92;
    padding: 5px;
    margin: 0px;
    color: #4B4F56;
    border-radius: 5px;
    font-family: monospace;
    font-weight: 500;
    font-size: 19px;
  }

  .fa-user-edit{
    color: white;
  }

  .invalid textarea{
    /*   border:2px #d81e38 solid;*/
    box-shadow: 0px 1px 6px 2px rgba(255, 0, 0, 0.7);
    transition: box-shadow 1s;
  }

  .card-text{
    font-family: monospace !important;
    font-size: 18px;
    width: 100%;
    height: 150px;
    outline: none;
    border: none;
    resize: none;
    border-radius: 10px;
    padding: 5px;
    margin-top: 5px;
    transition: background-color, box-shadow 0.7s;
    box-shadow: 0px 1px 7px 0px rgba(255, 118, 0, 1);
    border:2px #e9ebee solid;
  }

  .card-text:hover{
  }

  .btn-post{
    display: inline-block;
    background-color: #00CC67;
    width: 15%;
    height: 35px;
    text-align: center;
    border-radius: 25px;
    float: right;
    outline: none;
    transition: background-color, box-shadow 0.7s;
  }

  .btn-post:hover{
    background-color: #069850;
    box-shadow: 1px 1px 2px 2px rgba(245,134,52,0.7)
  }

  .btn-post-span{
    font-family: monospace !important;
    color: white;
    font-style: normal;
    font-size: 20px;
    font-weight: normal;
    font-variant: normal;
    line-height: 1;
  }

</style>
