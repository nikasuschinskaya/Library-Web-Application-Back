using AutoMapper;
using Library.Domain.Entities;
using Library.Presentation.Responses;

namespace Library.Presentation.Mappers.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>()
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
    }
}
