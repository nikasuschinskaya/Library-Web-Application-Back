using AutoMapper;
using Library.Domain.Entities;
using Library.Presentation.Requests;
using Library.Presentation.Responses;

namespace Library.Presentation.Mappers.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorRequest, Author>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

        CreateMap<Author, AuthorResponse>();
    }
}
