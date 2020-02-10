using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuessApp.DAL.ProfileRepoDAL;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;

namespace GuessApp.BLL.ProfileRepoBLL
{
    public class ProfileBLL : IProfileBLL
    {
        private readonly IProfileDal _profile;
        private readonly IMapper _mapper;

        public ProfileBLL(IProfileDal profile, IMapper mapper)
        {
            this._profile = profile;
            this._mapper = mapper;
        }

        public async Task<ProfileStatisticDto> getProfileInfoByUserId(string userId)
        {
            ProfileStatisticDto userCondition = await _profile.getProfileInfoByUserId(userId);
           // ProfileStatisticDto profileStatisticDto = _mapper.Map<ProfileStatisticDto>(userCondition);
            return userCondition;
        }

        public async Task<List<UserDto>> getUsersList(string searchText)
        {
            List<User> user = await _profile.getUsersList(searchText);
            return _mapper.Map<List<UserDto>>(user);
        }
    }
}
