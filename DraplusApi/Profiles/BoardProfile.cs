using AutoMapper;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Board, BoardReadDto>();
            CreateMap<BoardCreateDto, Board>();

            // For list of boards
            CreateMap<Board, BoardForListDto>();
        }
    }
}