using GuessApp.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuessApp.BLL.ProfileRepoBLL
{
    public interface IProfileBLL
    {
        Task<ProfileStatisticDto> getProfileInfoByUserId(string userId);
        Task<List<UserDto>> getUsersList(string searchText);

    }
}
