using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using Microsoft.EntityFrameworkCore;

namespace GuessApp.DAL.ProfileRepoDAL
{
    public class ProfileDal : IProfileDal
    {
        private readonly DbGuessContext _ctx;

        public ProfileDal(DbGuessContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<ProfileStatisticDto> getProfileInfoByUserId(string userId)
        {
            int userIdBySlug = await _ctx.User.Where(m => m.UserSlug == userId).
                                    Select(m=>m.UserId).FirstOrDefaultAsync();
            //  UserCondition userCondition = await _ctx.UserCondition.Where(m => m.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();
            ProfileStatisticDto userCondition = await _ctx.UserCondition.Include(m => m.User)
                                                 .Where(m => m.UserId == userIdBySlug)
                                                 .Select(x => new ProfileStatisticDto()
                                                 {
                                                     UserName = x.User.UserName,
                                                     UserId = x.User.UserId,
                                                     SuccessCount = x.SuccessCount,
                                                     FailureCount = x.FailureCount,
                                                     UnknownCount = x.UnknownCount,
                                                     UserPhone = x.User.UserPhone                                                                                         
                                                 }).FirstOrDefaultAsync();                

            return userCondition;
        }

        public async Task<List<User>> getUsersList(string searchText)
        {
            List<User> user = await _ctx.User.Where(m => (m.Name +" "+ m.Surname).ToLower().Contains(searchText.ToLower())).ToListAsync();
            return user;
        }
    }
}
