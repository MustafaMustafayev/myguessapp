using GuessApp.DTO.AuthViewModels;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GuessApp.DAL.AuthRepoDAL
{
    public interface IAuthDAL
    {
        Task<User> login(LoginDto loginDto);

        Task<User> loginDeletedUser(LoginDto loginDto);

        Task<User> getUserByUserName(string userName);

        Task<HttpStatusCode> addToken(Token token);

        Task<User> getUserByToken(string token);

        Task<HttpStatusCode> register(User user);

        Task<int> forgotPassword(ForgotPasswordDto forgotPasswordDto);

        Task<HttpStatusCode> updatePassword(UpdatePasswordDto updatePasswordDto);

        Task<User> getUserByUserId(string userId);

        Task<HttpStatusCode> updateUser(User user);

        Task<HttpStatusCode> deleteUser(int userId);

    }
}
