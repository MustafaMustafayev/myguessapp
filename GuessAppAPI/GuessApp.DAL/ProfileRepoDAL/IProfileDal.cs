using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuessApp.DAL.ProfileRepoDAL
{
    public interface IProfileDal
    {
        Task<ProfileStatisticDto> getProfileInfoByUserId(string userId);
        Task<List<User>> getUsersList(string searchText);
    }
}
