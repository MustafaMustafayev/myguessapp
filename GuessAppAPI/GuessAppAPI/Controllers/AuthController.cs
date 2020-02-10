using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using GuessApp.BLL.AuthRepoBLL;
using GuessApp.DTO.AuthViewModels;
using GuessApp.DTO.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GuessAppAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBLL _auth;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _iconfig;

        public AuthController(IAuthBLL auth, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            this._auth = auth;
            this._iconfig = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                UserDto invaliUserDto = new UserDto();
                invaliUserDto.StatusCode = "-3";
                invaliUserDto.responseText = "Invalid model";

                return Ok(invaliUserDto);          
            }
            UserDto userDto = await _auth.login(loginDto);

            if (userDto.StatusCode=="1")
            {

                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString()));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig.GetSection("JWTSettings:SecretKey").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                string tokenValue = tokenHandler.WriteToken(token);

                Token tokenModel = new Token()
                {
                    UserId = userDto.UserId,
                    TokenValue = tokenValue
                };

                HttpStatusCode responseToken = await _auth.addToken(tokenModel);
                if (responseToken != HttpStatusCode.Created)
                {
                    return Ok();
                }
                userDto.token = tokenModel.TokenValue;
            }
            return Ok(userDto);

        }

        [AllowAnonymous]
        [HttpPost("userByToken")]
        public async Task<IActionResult> userByToken([FromBody] TokenDto tokenDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
            UserDto userDto = await _auth.getUserByToken(tokenDto.TokenValue);
            return Ok(userDto);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> logout([FromBody] LogoutDto logoutDto)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            if (!ModelState.IsValid)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
            HttpContext.Response.Cookies.Delete(".AspNetCore.Security.Cookie");
            return Ok("Success");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(HttpStatusCode.BadRequest);
            }
            HttpStatusCode registerResponse = await _auth.register(registerDto);
            return Ok(registerResponse);
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> forgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(0);
            }

            return Ok(await _auth.forgotPassword(forgotPasswordDto));
        }

        [AllowAnonymous]
        [HttpPut("updatePassword")]
        public async Task<IActionResult> updatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(HttpStatusCode.BadRequest);
            }
            return Ok(await _auth.updatePassword(updatePasswordDto));
        }


        /// <summary>
        /// userId is slug
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("userById/{userId}")]
        public async Task<IActionResult> userById(string userId)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            return Ok(await _auth.getUserByUserId(userId));
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> updateUser([FromBody] RegisterDto registerDto)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            if (!ModelState.IsValid)
            {
                return Ok(HttpStatusCode.BadRequest);
            }
            registerDto.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(await _auth.updateUser(registerDto));
        }

        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> deleteUser(int userId)
        {
            //if ((User.FindFirst(ClaimTypes.NameIdentifier).Value) != Request.Headers["UserId"])
            //{
            //    return Unauthorized();
            //}
            return Ok(await _auth.deleteUser(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

    }
}