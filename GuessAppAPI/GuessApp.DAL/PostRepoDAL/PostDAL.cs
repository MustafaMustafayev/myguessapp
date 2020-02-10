using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using Microsoft.EntityFrameworkCore;

namespace GuessApp.DAL.PostRepoDAL
{
    public class PostDAL : IPostDAL
    {
        private readonly DbGuessContext _ctx;
        public PostDAL(DbGuessContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<HttpStatusCode> addPost(Post post)
        {
            try
            {
                post.CreatedDate = DateTime.Now;
             //   post.PostSlug = user.Name + "+" + user.Surname + " " + Guid.NewGuid().ToString();
                post.UpdatedDate = null;
                post.DeletedDate = null;
                _ctx.Post.Add(post);
                await _ctx.SaveChangesAsync();

                UserCondition userCondition = await _ctx.UserCondition.Where(m => m.UserId == post.UserId).FirstOrDefaultAsync();
                userCondition.UnknownCount += 1;
                _ctx.Update(userCondition);
                await _ctx.SaveChangesAsync();

                return HttpStatusCode.Created;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

        }

        public async Task<UserCondition> deletePost(DeletePostDto deletePostDto)
        {
            Post post = await _ctx.Post.Where(m => m.UserId == deletePostDto.UserId
                        && m.PostId == deletePostDto.PostId).FirstOrDefaultAsync();
            UserCondition userCondition = new UserCondition();
            if (post != null)
            {
                post.DeletedDate = DateTime.Now;

                _ctx.Update(post);
                int response = await _ctx.SaveChangesAsync();
                if (response > 0)
                {
                    userCondition = await _ctx.UserCondition.Where(m => m.UserId == deletePostDto.UserId).FirstOrDefaultAsync();

                    if (post.Condition == 0)
                    {
                        userCondition.UnknownCount -= 1;
                    }
                    else if (post.Condition == 1)
                    {
                        userCondition.SuccessCount -= 1;
                    }
                    else
                    {
                        userCondition.FailureCount -= 1;
                    }
                    _ctx.UserCondition.Update(userCondition);
                    int conditionResponse = await _ctx.SaveChangesAsync();
                    if (conditionResponse > 0)
                    {
                        return userCondition;
                    }
                }
            }

            return userCondition;
        }

        public async Task<string> getPostImagePathByPostId(int postId)
        {
            string postImagePath = await _ctx.Post.Where(m => m.PostId == postId).Select(m => m.PostImagePath).FirstOrDefaultAsync();
            return postImagePath;
        }

        public async Task<List<AllPostDto>> getUsersPostList(int pageNumber)
        {
            List<AllPostDto> userPosts = await (from u in _ctx.User
                                                join p in _ctx.Post
                                                on u.UserId equals p.UserId
                                                join uc in _ctx.UserCondition
                                                on u.UserId equals uc.UserId
                                                where u.DeletedDate == null
                                                && p.DeletedDate == null
                                                orderby p.CreatedDate descending
                                                select new AllPostDto()
                                                {
                                                    UserId = u.UserId,
                                                    UserName = u.UserName,
                                                    Name = u.Name,
                                                    Surname = u.Surname,
                                                    PostId = p.PostId,
                                                    PostCreatedDate = p.CreatedDate.ToString().Replace('T', ' '),
                                                    PostContent = p.PostContent,
                                                    PostCondition = p.Condition,
                                                    UserConditionId = uc.UserConditionId,
                                                    SuccessCount = uc.SuccessCount,
                                                    FailureCount = uc.FailureCount,
                                                    UnknownCount = uc.UnknownCount,
                                                    UserSlug = u.UserSlug,
                                                    PostImagePath = p.PostImagePath
                                                }).Skip((pageNumber - 1)*10).Take(10).ToListAsync();
            return userPosts;

        }

        public async Task<List<AllPostDto>> getUsersPostListByUserId(string userId, int pageNumber)
        {

            int userIdBySlug = await _ctx.User.Where(m => m.UserSlug == userId)
                        .Select(m => m.UserId).FirstOrDefaultAsync();

            List<AllPostDto> userPosts = await (from u in _ctx.User
                                                join p in _ctx.Post
                                                on u.UserId equals p.UserId
                                                join uc in _ctx.UserCondition
                                                on u.UserId equals uc.UserId
                                                where u.DeletedDate == null
                                                && p.DeletedDate == null
                                                && u.UserId == userIdBySlug
                                                orderby p.CreatedDate descending
                                                select new AllPostDto()
                                                {
                                                    UserId = u.UserId,
                                                    UserName = u.UserName,
                                                    Name = u.Name,
                                                    Surname = u.Surname,
                                                    PostId = p.PostId,
                                                    PostCreatedDate = p.CreatedDate.ToString().Replace('T', ' '),
                                                    PostContent = p.PostContent,
                                                    PostCondition = p.Condition,
                                                    UserConditionId = uc.UserConditionId,
                                                    SuccessCount = uc.SuccessCount,
                                                    FailureCount = uc.FailureCount,
                                                    UnknownCount = uc.UnknownCount,
                                                    UserSlug = u.UserSlug,
                                                    PostImagePath = p.PostImagePath
                                                }).Skip((pageNumber - 1)*10).Take(10).ToListAsync();

            return userPosts;
        }

        public async Task<UserCondition> updateConditionOfPost(PostConditionDto postConditionDto)
        {
            Post post = await _ctx.Post.Where(m => m.PostId == postConditionDto.PostId
                                            && m.UserId == postConditionDto.UserId).FirstOrDefaultAsync();


            UserCondition userCondition = new UserCondition();
            if (post != null)
            {
                int previousCondition = post.Condition;

                post.Condition = postConditionDto.Condition;
                post.UpdatedDate = DateTime.Now;

                _ctx.Post.Update(post);
                int row = await _ctx.SaveChangesAsync();

                userCondition = await _ctx.UserCondition.Where(m => m.UserId == postConditionDto.UserId).FirstOrDefaultAsync();

                if (userCondition != null)
                {

                    if (postConditionDto.Condition == 1)
                    {
                        if (previousCondition == 0)
                        {
                            userCondition.SuccessCount += 1;
                            userCondition.UnknownCount -= 1;
                        }
                        else if (previousCondition == -1)
                        {
                            userCondition.SuccessCount += 1;
                            userCondition.FailureCount -= 1;
                        }

                        _ctx.UserCondition.Update(userCondition);
                        int rowUserCondition = await _ctx.SaveChangesAsync();
                    }
                    else if (postConditionDto.Condition == -1)
                    {
                        if (previousCondition == 0)
                        {
                            userCondition.FailureCount += 1;
                            userCondition.UnknownCount -= 1;
                        }
                        else if (previousCondition == 1)
                        {
                            userCondition.FailureCount += 1;
                            userCondition.SuccessCount -= 1;
                        }

                        _ctx.UserCondition.Update(userCondition);
                        int rowUserCondition = await _ctx.SaveChangesAsync();

                        if (rowUserCondition > 0)
                        {
                            userCondition = await _ctx.UserCondition.Where(m => m.UserId == postConditionDto.UserId).FirstOrDefaultAsync();
                        }
                    }
                }
            }

            return userCondition;
        }

        public async Task<UpdatePostDto> updatePost(UpdatePostDto updatePostDto)
        {
            Post post = await _ctx.Post.Where(m => m.UserId == updatePostDto.UserId
                                && m.PostId == updatePostDto.PostId
                                && m.DeletedDate == null).FirstOrDefaultAsync();
            if (post != null)
            {

                post.UpdatedDate = DateTime.Now;
                post.PostContent = updatePostDto.PostContent;
                _ctx.Post.Update(post);
                int rowNumber = await _ctx.SaveChangesAsync();
                if (rowNumber > 0)
                {
                    return updatePostDto;
                }
            }
            UpdatePostDto response = new UpdatePostDto();
            return response;
        }
    }
    }