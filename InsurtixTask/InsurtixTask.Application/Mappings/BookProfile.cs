using AutoMapper;
using InsurtixTask.Application.DTOs;
using InsurtixTask.Application.RequestObjects;
using InsurtixTask.Domain.Entities;

namespace InsurtixTask.Application.Mappings;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookRequest, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new Domain.Entities.Title
            {
                Language = src.Title.Language,
                Value = src.Title.Value
            }))

            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))

            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))

            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))

            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));

        CreateMap<Book, BookDTO>()
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))

            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))

            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))

            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))

            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new Application.DTOs.Title
            {
                Language = src.Title.Language,
                Value = src.Title.Value
            }));
    }
}
