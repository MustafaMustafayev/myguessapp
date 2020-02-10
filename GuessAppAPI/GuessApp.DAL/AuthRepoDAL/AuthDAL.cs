using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GuessApp.Core.Util;
using GuessApp.DTO.AuthViewModels;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using Microsoft.EntityFrameworkCore;

namespace GuessApp.DAL.AuthRepoDAL
{
    public class AuthDAL : IAuthDAL
    {
        private readonly DbGuessContext _ctx;

        public AuthDAL(DbGuessContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<HttpStatusCode> addToken(Token token)
        {
            try
            {
                _ctx.Token.Add(token);
                await _ctx.SaveChangesAsync();
                return HttpStatusCode.Created;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> deleteUser(int userId)
        {
            User user = await _ctx.User.Where(m => m.UserId == userId).FirstOrDefaultAsync();
            _ctx.User.Remove(user);
            int response = await _ctx.SaveChangesAsync();
            if (response > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<int> forgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            User user = await _ctx.User.Where(m => m.UserName == forgotPasswordDto.UserName
                                && m.UserMail == forgotPasswordDto.UserMail
                                && m.DeletedDate == null).FirstOrDefaultAsync();

            if (user != null)
            {
                return user.UserId;
            }
            return 0;

        }

        public async Task<User> getUserByToken(string token)
        {
            int userId = await _ctx.Token.Where(m => m.TokenValue == token).Select(m => m.UserId).FirstOrDefaultAsync();
            return await _ctx.User.Where(m => m.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<User> getUserByUserId(string userId)
        {
            int userIdBySlug = await _ctx.User.Where(m => m.UserSlug == userId)
                                .Select(m => m.UserId).FirstOrDefaultAsync();

            return await _ctx.User.Where(m => m.UserId == userIdBySlug && m.DeletedDate == null).FirstOrDefaultAsync();

        }

        public async Task<User> getUserByUserName(string userName)
        {
            return await _ctx.User.Where(m => m.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<User> login(LoginDto loginDto)
        {
            return await _ctx.User.Where(m => m.UserName == loginDto.UserName && m.UserPassword == loginDto.UserPassword
                                            && m.DeletedDate == null).FirstOrDefaultAsync();
        }

        public async Task<User> loginDeletedUser(LoginDto loginDto)
        {
            return await _ctx.User.Where(m => m.UserName == loginDto.UserName && m.UserPassword == loginDto.UserPassword).FirstOrDefaultAsync();
        }

        public async Task<HttpStatusCode> register(User user)
        {
            user.UserSlug = SlugifyParameterTransformer.GenerateSlug(user.UserMail) ;
            _ctx.User.Add(user);
            int response = await _ctx.SaveChangesAsync();

            if (response > 0)
            {
                UserCondition userCondition = new UserCondition()
                {
                    UserId = user.UserId,
                    SuccessCount = 0,
                    FailureCount = 0,
                    UnknownCount = 0
                };

                _ctx.UserCondition.Add(userCondition);
                int responseCondition = await _ctx.SaveChangesAsync();

                if (responseCondition > 0)
                {
                    return HttpStatusCode.OK;
                }
            }

            return HttpStatusCode.InternalServerError;
        }

        public async Task<HttpStatusCode> updatePassword(UpdatePasswordDto updatePasswordDto)
        {
            User user = await _ctx.User.Where(m => m.UserId == updatePasswordDto.UserId
                                && m.DeletedDate == null).FirstOrDefaultAsync();

            if (user != null)
            {
                user.UserPassword = updatePasswordDto.UpdatedPassword;
                user.UpdatedDate = DateTime.Now;
                _ctx.Update(user);
                int response = await _ctx.SaveChangesAsync();

                if (response > 0)
                {
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.BadRequest;

        }

        public async Task<HttpStatusCode> updateUser(User user)
        {
            user.UserSlug = await _ctx.User.Where(m => m.UserId == user.UserId).
                            Select(m => m.UserSlug).FirstOrDefaultAsync();
            user.UpdatedDate = DateTime.Now;
            _ctx.User.Update(user);
            int response = await _ctx.SaveChangesAsync();
            if (response > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}
