using GuessApp.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GuessApp.BLL.PostRepoBLL
{
    public interface IPostBLL
    {
        Task<HttpStatusCode> addPost(PostDto postDto);

        Task<List<AllPostDto>> getUsersPostList(int pageNumber);

        Task<List<AllPostDto>> getUsersPostListByUserId(string userId, int pageNumber);

        Task<ProfileStatisticDto> updateConditionOfPost(PostConditionDto postConditionDto);

        Task<ProfileStatisticDto> deletePost(DeletePostDto deletePostDto);

        Task<UpdatePostDto> updatePost(UpdatePostDto updatePostDto);

        Task<string> getPostImagePathByPostId(int postId);
    }
}
