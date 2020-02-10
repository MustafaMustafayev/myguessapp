using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuessApp.DAL.PostRepoDAL;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;

namespace GuessApp.BLL.PostRepoBLL
{
    public class PostBLL : IPostBLL
    {
        private readonly IPostDAL _post;
        private readonly IMapper _mapper;

        public PostBLL(IPostDAL post, IMapper mapper)
        {
            _post = post;
            _mapper = mapper;
        }

        public async Task<HttpStatusCode> addPost(PostDto postDto)
        {
            Post post = _mapper.Map<Post>(postDto);
            return await _post.addPost(post);
        }

        public async Task<ProfileStatisticDto> deletePost(DeletePostDto deletePostDto)
        {
            UserCondition userCondition = await _post.deletePost(deletePostDto);
            ProfileStatisticDto profileStatisticDto = _mapper.Map<ProfileStatisticDto>(userCondition);
            return profileStatisticDto;
        }

        public Task<string> getPostImagePathByPostId(int postId)
        {
            return _post.getPostImagePathByPostId(postId);
        }

        public async Task<List<AllPostDto>> getUsersPostList(int pageNumber)
        {
            return await _post.getUsersPostList(pageNumber);
         }

        public async Task<List<AllPostDto>> getUsersPostListByUserId(string userId, int pageNumber)
        {
            return await _post.getUsersPostListByUserId(userId, pageNumber);
        }

        public async Task<ProfileStatisticDto> updateConditionOfPost(PostConditionDto postConditionDto)
        {
            UserCondition userCondition = await _post.updateConditionOfPost(postConditionDto);
            ProfileStatisticDto profileStatisticDto = _mapper.Map<ProfileStatisticDto>(userCondition);

            return profileStatisticDto;
        }

        public async Task<UpdatePostDto> updatePost(UpdatePostDto updatePostDto)
        {
            return await _post.updatePost(updatePostDto);
        }
    }
}
