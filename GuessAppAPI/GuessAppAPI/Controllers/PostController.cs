using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using GuessApp.BLL.PostRepoBLL;
using GuessApp.DTO.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessAppAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostBLL _post;
        private readonly IHostingEnvironment _environment;

        public PostController(IPostBLL post, IHostingEnvironment environment)
        {
            _post = post;
            _environment = environment;
        }


        //[AllowAnonymous]
        [HttpPost("addPostImage")]
        public async Task<IActionResult> addPostImage([FromForm] PostModel postModel)
        {
            Guid guid = Guid.NewGuid();
            var uploads = Path.Combine(_environment.WebRootPath, "uploads/posts");
            string fileName = guid.ToString() + postModel.file.FileName;
            string filePath = "/uploads/posts/" + fileName;
            if (postModel.file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    await postModel.file.CopyToAsync(fileStream);
                }
            }

            return Ok(filePath);
        }

        [HttpPost("addPost")]
        public async Task<IActionResult> addPost([FromBody] PostDto postDto)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            postDto.UserId = (User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!ModelState.IsValid)
            {
                return Ok("-1");
            }
            HttpStatusCode response = await _post.addPost(postDto);
            if (response != HttpStatusCode.Created)
            {
                return Ok("0");
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("usersPosts/{pageNumber}")]
        public async Task<IActionResult> usersPosts(int pageNumber)
        {
            List<AllPostDto> allPostDtos = await _post.getUsersPostList(pageNumber);

            foreach(var item in allPostDtos)
            {
                if (item.PostImagePath.Trim().Length > 0)
                {
                    string path = _environment.WebRootPath + item.PostImagePath.Trim();
                    byte[] base64File = System.IO.File.ReadAllBytes(path);
                    item.PostImagePath = "data:image/png;base64," + Convert.ToBase64String(base64File);
                }
            }

            return Ok(allPostDtos);
        }


        /// <summary>
        /// userId is slug
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("usersPostsByUserId/{userId}/{pageNumber}")]
        public async Task<IActionResult> usersPostsByUserId(string userId,int pageNumber)
        {
            List<AllPostDto> posts = await _post.getUsersPostListByUserId(userId, pageNumber);

            foreach (var item in posts)
            {
                if (item.PostImagePath.Trim().Length > 0)
                {
                    string path = _environment.WebRootPath + item.PostImagePath.Trim();
                    byte[] base64File = System.IO.File.ReadAllBytes(path);
                    item.PostImagePath = "data:image/png;base64," + Convert.ToBase64String(base64File);
                }
            }

            return Ok(posts);
        }

        [HttpPost("updatePostCondition")]
        public async Task<IActionResult> updatePostCondition([FromBody] PostConditionDto postConditionDto)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            postConditionDto.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ProfileStatisticDto profileStatisticDto =  await _post.updateConditionOfPost(postConditionDto);
            return Ok(profileStatisticDto);
        }

        [HttpPost("deletePost")]
        public async Task<IActionResult> deletePost([FromBody] DeletePostDto deletePostDto)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            deletePostDto.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ProfileStatisticDto userCondition = await _post.deletePost(deletePostDto);
            if (userCondition.UserId > 0)
            {
                string postImagePath = await _post.getPostImagePathByPostId(deletePostDto.PostId);
                if (postImagePath.Trim().Length > 0)
                {
                    string path = _environment.WebRootPath + postImagePath;
                    System.IO.File.Delete(path);
                }
            }
            return Ok(userCondition);
        }

        [HttpPut("updatePost")]
        public async Task<IActionResult> updatePost([FromBody] UpdatePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            postDto.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                
            return Ok(await _post.updatePost(postDto));
        }

    }
}