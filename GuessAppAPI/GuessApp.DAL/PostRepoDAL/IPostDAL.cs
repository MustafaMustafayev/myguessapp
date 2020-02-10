using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GuessApp.DAL.PostRepoDAL
{
    public interface IPostDAL
    {
        Task<HttpStatusCode> addPost(Post post);
        
        Task<List<AllPostDto>> getUsersPostList(int pageNumber);

        Task<List<AllPostDto>> getUsersPostListByUserId(string userId, int pageNumber);

        Task<UserCondition> updateConditionOfPost(PostConditionDto postConditionDto);

        Task<UserCondition> deletePost(DeletePostDto deletePostDto);

        Task<UpdatePostDto> updatePost(UpdatePostDto updatePostDto);

        Task<string> getPostImagePathByPostId(int postId);

    }
}
