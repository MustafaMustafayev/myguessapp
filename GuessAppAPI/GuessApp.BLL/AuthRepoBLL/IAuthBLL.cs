using GuessApp.DTO.AuthViewModels;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GuessApp.BLL.AuthRepoBLL
{
    public interface IAuthBLL
    {
        Task<UserDto> login(LoginDto loginDto);
        Task<HttpStatusCode> addToken(Token token);
        Task<UserDto> getUserByToken(string token);
        Task<HttpStatusCode> register(RegisterDto registerDto);
        Task<int> forgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<HttpStatusCode> updatePassword(UpdatePasswordDto updatePasswordDto);
        Task<RegisterDto> getUserByUserId(string userId);
        Task<HttpStatusCode> updateUser(RegisterDto registerDto);
        Task<HttpStatusCode> deleteUser(int userId);


    }
}
