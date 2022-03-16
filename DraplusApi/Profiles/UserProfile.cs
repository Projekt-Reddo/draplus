using AutoMapper;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserManageListDto>();
        }
    }
}