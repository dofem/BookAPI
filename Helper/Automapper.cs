using AutoMapper;
using BookAPI.Dto;
using BookAPI.Model;

namespace BookAPI.AutoMapper
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<CreateBook, Book>().ReverseMap();
            CreateMap<CreateAuthor, Author>().ReverseMap();
            CreateMap<CreatePublisher, Publisher>().ReverseMap();
        }
    }
}
