using AutoMapper;
using Library.Domain.Entities;

namespace Library.Application.Mappers.Profiles;

public class BookEntityProfile : Profile
{
    public BookEntityProfile()
    {
        CreateMap<Book, Book>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
          .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
          .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
          .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
          .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
          .ForMember(dest => dest.ImageURL, opt => opt.Ignore())
          .ForMember(dest => dest.UserBooks, opt => opt.Ignore())
          .ForMember(dest => dest.BookStockStatus, opt => opt.Ignore())
          .ForMember(dest => dest.Authors, opt => opt.Ignore())
          ;
    }
}
