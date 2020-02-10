using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GuessApp.BLL.ProfileRepoBLL;
using GuessApp.DTO.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessAppAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileBLL _profile;

        public ProfileController(IProfileBLL profile)
        {
            this._profile = profile;
        }


        /// <summary>
        /// userId is slug
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("profileInfo/{userId}")]
        public async Task<IActionResult> profileInfo(string userId)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            ProfileStatisticDto profileDto = await _profile.getProfileInfoByUserId(userId);
            return Ok(profileDto);
        }

        [HttpGet("usersList/{searchText}")]
        public async Task<IActionResult> usersList(string searchText)
        {
            
            return Ok(await _profile.getUsersList(searchText));
        }
    }
}