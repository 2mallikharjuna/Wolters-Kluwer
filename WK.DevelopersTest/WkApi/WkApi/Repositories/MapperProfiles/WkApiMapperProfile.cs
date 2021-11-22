using System;
using AutoMapper;
using WkApi.Domain.DTO;
using WkApi.Domain.Models;

namespace WkApi.Repositories.MapperProfiles
{
    public class WkApiMapperProfile : Profile
    {
        public WkApiMapperProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
