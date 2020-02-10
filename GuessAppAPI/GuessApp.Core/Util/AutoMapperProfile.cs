using AutoMapper;
using GuessApp.DTO.ViewModels;
using GuessAppAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessApp.Core.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<PostDto, Post>();
            CreateMap<UserCondition, ProfileStatisticDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<User, RegisterDto>();

        }

        //Scaffold-DbContext "Server=DESKTOP-DPCC3SS\MUSTAFA;Database=DbGuess;Trusted_Connection=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir D:\MyFiles\MyProjects\GuessAppAPI\GuessApp.Entities\UpdatedEntities
    }
}
