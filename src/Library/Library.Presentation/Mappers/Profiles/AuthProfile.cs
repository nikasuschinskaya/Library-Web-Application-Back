using AutoMapper;
using Library.Domain.Models;
using Library.Presentation.Responses;

namespace Library.Presentation.Mappers.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<AuthTokens, AuthTokensResponse>();
    }
}
