using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Extentions;
using Library.Presentation.Requests;
using Library.Presentation.Responses;

namespace Library.Presentation.Mappers.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookListResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.BookStockStatus, opt => opt.MapFrom(src => src.BookStockStatus.StringValue()))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.Name + " " + a.Surname))))
            ;

        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
            .ForMember(dest => dest.BookStockStatus, opt => opt.MapFrom(src => src.BookStockStatus.StringValue()))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            ;

        CreateMap<BookRequest, Book>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
           .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
           .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
           //.ForMember(dest => dest.Authors, opt => opt.Ignore())
           ;

        CreateMap<BookAddRequest, Book>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
           .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
           .ForMember(dest => dest.Authors, opt => opt.Ignore())
           ;
    }
}
