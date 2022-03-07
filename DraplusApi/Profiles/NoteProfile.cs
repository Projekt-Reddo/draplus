using AutoMapper;
using DraplusApi.Dtos;
using DraplusApi.Models;

namespace DraplusApi.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}