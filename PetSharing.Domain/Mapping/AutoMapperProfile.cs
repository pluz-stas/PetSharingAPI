using AutoMapper;
using PetSharing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Domain.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, Models.User>();
            CreateMap<Models.User, User>();
        }
    }
}
