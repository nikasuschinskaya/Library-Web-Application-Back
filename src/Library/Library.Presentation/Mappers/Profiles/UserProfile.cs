using AutoMapper;
using Library.Domain.Entities;
using Library.Presentation.Responses;

namespace Library.Presentation.Mappers.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
           .ForMember(dest => dest.UserBooks, opt => opt.MapFrom(src => src.UserBooks))
           ;
    }
}
