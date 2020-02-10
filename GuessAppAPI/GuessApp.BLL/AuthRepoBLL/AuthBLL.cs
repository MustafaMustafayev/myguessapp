using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuessApp.DAL.AuthRepoDAL;
using GuessApp.DTO.AuthViewModels;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;

namespace GuessApp.BLL.AuthRepoBLL
{
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthDAL _auth;
        private readonly IMapper _mapper;

        public AuthBLL(IAuthDAL auth, IMapper mapper)
        {
            this._auth = auth;
            this._mapper = mapper;
        }

        public async Task<HttpStatusCode> addToken(Token token)
        {
            return await _auth.addToken(token);
        }

        public async Task<HttpStatusCode> deleteUser(int userId)
        {
            return await _auth.deleteUser(userId);
        }

        public async Task<int> forgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            return await _auth.forgotPassword(forgotPasswordDto);
        }

        public async Task<UserDto> getUserByToken(string token)
        {
            User user =  await _auth.getUserByToken(token);
            UserDto userDto = new UserDto();
            if (user != null)
            {
                userDto = _mapper.Map<UserDto>(user);
                userDto.token = token;
                userDto.StatusCode = "byToken";
                userDto.responseText = "byToken";
            }
            else
            {
                userDto.StatusCode = "-3";
                userDto.responseText = "User is not logged in";
            }
            return userDto;
        }

        public async Task<RegisterDto> getUserByUserId(string userId)
        {
            User user = await _auth.getUserByUserId(userId);
            RegisterDto registerDto = new RegisterDto();
            if (user != null)
            {
                registerDto = _mapper.Map<RegisterDto>(user);
            }
            return registerDto;
        }

        public async Task<UserDto> login(LoginDto loginDto)
        {
            User userByUserName = await _auth.getUserByUserName(loginDto.UserName);

            if (userByUserName == null)
            {
                UserDto userDtoByUserName = new UserDto();
                userDtoByUserName.StatusCode = "-2";
                userDtoByUserName.responseText = "User does not exist";
                return userDtoByUserName;
            }
            else
            {
                User deletedUser = await _auth.loginDeletedUser(loginDto);
                if (deletedUser == null)
                {
                    UserDto userDto = new UserDto();
                    userDto.StatusCode = "-1";
                    userDto.responseText = "Password is incorrect";
                    return userDto;
                }
                else
                {
                    User user = await _auth.login(loginDto);
                    if (user == null)
                    {
                        UserDto userDto = new UserDto();
                        userDto.StatusCode = "0";
                        userDto.responseText = "User is deactive";
                        return userDto;
                    }
                    else
                    {
                        UserDto userDto = _mapper.Map<UserDto>(user);
                        userDto.StatusCode = "1";
                        userDto.responseText = "Success";
                        return userDto;

                    }
                }
            }

        }

        public async Task<HttpStatusCode> register(RegisterDto registerDto)
        {
            User user = _mapper.Map<User>(registerDto);
            return await _auth.register(user);
        }

        public async Task<HttpStatusCode> updatePassword(UpdatePasswordDto updatePasswordDto)
        {
            return await _auth.updatePassword(updatePasswordDto);
        }

        public async Task<HttpStatusCode> updateUser(RegisterDto registerDto)
        {
            User user = _mapper.Map<User>(registerDto);
            return await _auth.updateUser(user);
        }
    }
}
