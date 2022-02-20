using AutoMapper;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthDto>();
        }
    }
}