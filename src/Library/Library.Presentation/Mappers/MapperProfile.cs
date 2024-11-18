//using AutoMapper;
//using Library.Domain.Entities;
//using Library.Domain.Enums.Extentions;
//using Library.Presentation.Requests;
//using Library.Presentation.Responses;

//namespace Library.Presentation.Mappers;

//public class MapperProfile : Profile
//{
//    public MapperProfile()
//    {
//        CreateMap<AuthTokens, AuthTokensResponse>();

//        CreateMap<User, UserResponse>()
//            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
//            ;

//        CreateMap<Book, BookListResponse>()
//            .ForMember(dest => dest.BookStockStatus, opt => opt.MapFrom(src => src.BookStockStatus.StringValue()))
//            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
//            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.Name + " " + a.Surname))));

//        CreateMap<Book, BookResponse>()
//            .ForMember(dest => dest.BookStockStatus, opt => opt.MapFrom(src => src.BookStockStatus.StringValue()))
//            ;

//        CreateMap<AuthorRequest, Author>()
//            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
//            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
//            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
//            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
//            ;

//        CreateMap<BookRequest, Book>()
//           .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
//           ;

//        CreateMap<Author, AuthorResponse>();
//        CreateMap<Genre, GenreResponse>();
//    }
//}