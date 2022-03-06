using AutoMapper;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Profiles
{
    public class ShapeProfile : Profile
    {
        public ShapeProfile()
        {
            CreateMap<Shape, ShapeReadDto>();
            CreateMap<ShapeCreateDto, Shape>();
            CreateMap<ShapeReadDto, Shape>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}