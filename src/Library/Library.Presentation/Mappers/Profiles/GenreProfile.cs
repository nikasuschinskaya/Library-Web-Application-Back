using AutoMapper;
using Library.Domain.Entities;
using Library.Presentation.Responses;

namespace Library.Presentation.Mappers.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreResponse>();
    }
}
