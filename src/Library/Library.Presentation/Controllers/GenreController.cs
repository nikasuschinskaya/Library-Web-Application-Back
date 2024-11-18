using AutoMapper;
using Library.Application.Interfaces.UseCases.Genres;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    //private readonly IGenreService _genreService;
    private readonly IGetAllGenresUseCase _getAllGenresUseCase;
    private readonly IGetGenreByIdUseCase _getGenreByIdUseCase;
    private readonly IMapper _mapper;

    public GenreController(IGetAllGenresUseCase getAllGenresUseCase,
                           IGetGenreByIdUseCase getGenreByIdUseCase,
                           IMapper mapper)
    {
        _getAllGenresUseCase = getAllGenresUseCase;
        _getGenreByIdUseCase = getGenreByIdUseCase;
        _mapper = mapper;
    }


    //public GenreController(IGenreService genreService, IMapper mapper)
    //{
    //    _genreService = genreService;
    //    _mapper = mapper;
    //}

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllGenres(CancellationToken cancellationToken = default)
    {
        //var genres = await _genreService.GetAllGenresAsync(cancellationToken);
        var genres = await _getAllGenresUseCase.ExecuteAsync(cancellationToken);
        var response = genres.Select(genre => _mapper.Map<GenreResponse>(genre));
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GenreResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetGenreById(Guid id, CancellationToken cancellationToken = default)
    {
        //var genre = await _genreService.GetGenreByIdAsync(id, cancellationToken);
        var genre = await _getGenreByIdUseCase.ExecuteAsync(id, cancellationToken);
        var response = _mapper.Map<GenreResponse>(genre);
        return Ok(response);
    }
}