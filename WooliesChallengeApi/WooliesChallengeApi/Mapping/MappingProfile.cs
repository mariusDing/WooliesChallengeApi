using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Users.Models;
using WooliesChallengeApi.ViewModels;

namespace WooliesChallengeApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserVM>();
        }
    }
}
