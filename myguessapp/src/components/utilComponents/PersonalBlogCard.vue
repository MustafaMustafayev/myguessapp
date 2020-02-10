<template>
  <div class="fluid">
    <div class="card" v-for="post in this.$store.state.personalPosts">
      <div class="card-body">
        <i class="fas fa-user-edit"></i>
        <h5 class="card-title">{{post.name}} {{post.surname}}</h5>
        <div class="user-count-pointer">
        <span class="successCount"><i class="fas fa-check-circle"></i> {{post.successCount}},</span>
        <span class="failureCount"><i class="fas fa-times-circle"></i> {{post.failureCount}},</span>
        <span class="unknownCount"><i class="fas fa-question-circle"></i> {{post.unknownCount}}</span>
        </div>
        <span class="timestampContent"><i class="fas fa-clock"></i> {{post.postCreatedDate}}</span>
        <pre v-if="post.postContent.trim().length>0" class="card-text">{{post.postContent}}</pre>

        <div class="public-post-img" v-if="post.postImagePath.trim().length>0">
          <img class="img-fluid public-img" :src="post.postImagePath" alt="Card image" style="">
        </div>

        <div class="user-edit-panel" v-if="isPersonalProfile">
          <button @click="showUpdatePostModal(post.postId, post.postContent)" title="Edit post"  class="personal-operation"><i class="fas fa-edit"></i></button>
          <button @click="deletePersonalPost(post.postId)" title="Delete post" class="personal-operation"><i class="fas fa-trash-alt"></i></button>
          <button @click="updatePostCondition(post,1)" title="Success guess" class="personal-operation successCount"><i class="fas fa-check-double"></i></button>
          <button @click="updatePostCondition(post,-1)" title="Failure guess" class="personal-operation failureCount"><i class="fas fa-times-circle"></i></button>
          <span v-if="post.postCondition==1" class="post-current-status success">Status: success</span>
          <span v-if="post.postCondition==-1" class="post-current-status failure">Status: failure</span>
          <span v-if="post.postCondition==0" class="post-current-status unknown">Status: unknown</span>
        </div>
      </div>
    </div>


    <div class="modal fade" id="postModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title"  id="postModalLongTitle">Guess edit</h5>
            <button type="button" class="close" style="text-align: center" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div :class="{'invalid': $v.updatedPostContent.$error}">
            <textarea v-model="updatedPostContent" @input="$v.updatedPostContent.$touch()" class="card-text-update"></textarea>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" @click="closePostUpdateModal" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" @click="updatePost" class="btn btn-success">Edit</button>
          </div>
        </div>
      </div>
    </div>


  </div>
</template>


<script>

  import { required, minLength, email, minValue, sameAs} from 'vuelidate/lib/validators'


  export default {
    data(){
      return{
        updatedPostContent:'',
        updatedPostId:'',
        isPersonalProfile:this.$route.params.id == this.$store.state.userSlug,
        routerParamUserId:this.$route.params.id,
        showModal:false
      }
    },
    validations:{
      updatedPostContent:{
        required,
        minLength:minLength(5)
      }
    },
    beforeMount() {

        this.$store.state.personalPageNumber = 1;
        this.$store.dispatch('getUsersPostsByUserId',this.$route.params.id);

    },
    watch:{
      'showModal':function(){

      },
      '$route.params.id':function(){
        this.$store.state.personalPageNumber = 1;
        this.$store.dispatch('getUsersPostsByUserId',this.$route.params.id);
        this.isPersonalProfile = this.$route.params.id == this.$store.state.userSlug;
      },
      '$store.state.userId':function () {
        this.isPersonalProfile = this.$route.params.id == this.$store.state.userSlug;
      }
    },
    mounted(){
      //this.$refs.postModalCenter.on('hidden.bs.modal', this.hiddenModal);
    },
    methods:{
      updatePostCondition(post, conditionId){

        const updatedPostCondition = {
          userId : this.$store.state.userId,
          postId : post.postId,
          condition : conditionId
        };
        this.$store.dispatch('changeStatusOfPersonalPost', updatedPostCondition);
        post.postCondition=conditionId;

      },
      deletePersonalPost(postId){

        const deletePost = {
          postId:postId,
          userId: this.$store.state.userId
        };

        this.$store.dispatch('deletePersonalPost',deletePost);

      },

      showUpdatePostModal(postId, postContent){
        this.updatedPostId = postId;
        this.updatedPostContent = postContent;
        this.showModal = true;
        $('#postModalCenter').modal('show');
      },

      updatePost(){

        const updatedPost = {
          postId: this.updatedPostId,
          postContent: this.updatedPostContent,
          userId: this.$store.state.userId
        };

        this.$store.dispatch('updatePost',updatedPost);
        $('#postModalCenter').modal('hide');
        this.updatedPostId = '';
        this.updatedPostContent = '';
      },

      closePostUpdateModal(){
        $('#postModalCenter').modal('hide');
        this.updatedPostId = '';
        this.updatedPostContent = '';
      }
    }
  }
</script>

<style scoped>

  .public-post-img{
    width: 100%;
  }

  .public-img{
    object-fit: cover;

  }

  .public-img:hover{

  }

  .invalid textarea{
    /*   border:2px #d81e38 solid;*/
    box-shadow: 0px 1px 6px 2px rgba(255, 0, 0, 0.7);
    transition: box-shadow 1s;
  }

  #postModalCenter{
    padding: 0px;
    margin: 0px;
  }

  #postModalCenter:after{
    padding: 0px !important;
    margin: 0px !important;
  }

  .success{
    color: #00CC67;
    font-style: italic;
    font-size: 16px;
    font-weight: 700;
  }

  .modal-title{
    color: #00CC67;
    font-family: monospace;
    font-size: 18px;
  }

  .card-text-update{
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

  .card-text-update:hover{

  }

  .failure{
    color: red;
    font-style: italic;
    font-size: 16px;
    font-weight: 700;
  }

  .unknown{
    font-style: italic;
    font-size: 16px;
    font-weight: 700;
    color: #0037ff;
  }

  .card-text-update{
    width: 100%;
    height: 150px;
    outline: none;
    border: none;
    resize: none;
    border-radius: 10px;
    padding: 5px;
    margin-top: 5px;
    transition: background-color 0.7s;
  }

  .card-text-update:hover{
    background-color:#f7f4f4;
  }

  .post-current-status{
    float: right;
  }

  .user-count-pointer{
    display: inline-block;
    float: right;
  }

  .successCount{
    color: #00CC67;
    font-weight: 700;
  }

  .failureCount{
    color: red;
    font-weight: 700;
  }

  .unknownCount{
    color: #0037ff;
    font-weight: 700;
  }

  .user-edit-panel{
    font-family: monospace !important;
    padding: 5px;
    height: 40px;
    width: 100%;
    height: 40px;
    background-color: rgba(249, 215, 153, 0.7);
    border-radius: 5px;
    opacity: 0.7;
    transition: background-color 1s;
    margin-top: 10px;
  }

  .user-edit-panel:hover{
    background-color: rgb(253,203,146);
    opacity: 1;
  }

  .personal-operation{
    /*float: right;*/
    margin-left: 2px;
    border-radius: 5px 5px;
    outline: none;
    background-color: cornsilk;
    transition: background-color 0.7s;
  }

  .personal-operation:hover{
    background-color: #f3e5ac;
  }

  .timestampContent{
    color: #616770;
    font-weight: normal;
    font-size: 12px;
    display: block;
  }

  .card-title{
    background-color: #fdcb92;
    padding: 5px;
    margin: 0px;
    color: #4B4F56;
    border-radius: 5px;
    display: inline-block;
  }

  .modal{
    box-shadow: 2px 2px lightblue;
  }

  .card-text{
    padding: 5px;
    color:black;
    font-size: 18px;
    width: 100%;
    height: 100%;
    outline: none;
    border: none;
    resize: none;
    border-radius: 10px;
    margin-top: 5px;
    font-style: unset;
    font-family: monospace,unset;
    box-shadow: 0px 1px 3px 0px rgba(71, 220, 12, 1);
    transition: box-shadow 0.7s;
  }

  .card-text:hover{
    box-shadow: 1px 3px 10px 2px rgba(71, 220, 12, 0.7)
  }

  .card{
    transition: box-shadow, background-color 0.7s;
    padding: 5px;
    margin-top: 20px;
    box-shadow: 1px 2px 7px 5px rgb(197, 187, 187);
  }

  .card:hover{
    box-shadow: 0px 0px 18px 6px rgba(245, 158, 13, 0.7);
    background-color:#f7f4f4;
  }

  .fa-user-edit{
    font-size: 25px;
    color: #d2d7ff;
  }
</style>


