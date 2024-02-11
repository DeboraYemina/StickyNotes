using AutoMapper;
using NotasApi.Models;
using NotasApi.Models.DTOs;

namespace NotasApi.Mapper
{
    public class NotasApiMapper:Profile
    {
        public NotasApiMapper()
        {
            CreateMap<Note, CreateNoteDTO>().ReverseMap();
            CreateMap<Note, NoteResponseDto>().ReverseMap();
            CreateMap<Tag, CreateTagDTO>().ReverseMap();
            CreateMap<Tag, TagResponseDTO>().ReverseMap();
        }
    }
}
