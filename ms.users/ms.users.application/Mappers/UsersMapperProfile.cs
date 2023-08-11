using AutoMapper;
using ms.users.application.Responses;
using ms.users.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.users.application.Mappers
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile() => CreateMap<User, UserResponse>().ReverseMap();
    }
}
