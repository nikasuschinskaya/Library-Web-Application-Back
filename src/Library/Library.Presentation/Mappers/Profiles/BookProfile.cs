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
           .ForMember(dest => dest.BookStockStatus, opt => opt.MapFrom(src => src.BookStockStatus.StringValue()))
           .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
           .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.Name + " " + a.Surname))));

        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.BookStockStatus, opt => opt.MapFrom(src => src.BookStockStatus.StringValue()));

        CreateMap<BookRequest, Book>()
           .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));

        CreateMap<BookAddRequest, Book>()
           .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
           .ForMember(dest => dest.Authors, opt => opt.Ignore());
    }
}
