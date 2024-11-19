using AutoMapper;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
    private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
    private readonly IAddAuthorUseCase _addAuthorUseCase;
    private readonly IUpdateAuthorUseCase _updateAuthorUseCase;
    private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
    private readonly IMapper _mapper;

    public AuthorController(IGetAllAuthorsUseCase getAllAuthorsUseCase,
                            IGetAuthorByIdUseCase getAuthorByIdUseCase,
                            IAddAuthorUseCase addAuthorUseCase,
                            IUpdateAuthorUseCase updateAuthorUseCase,
                            IDeleteAuthorUseCase deleteAuthorUseCase,
                            IMapper mapper)
    {
        _getAllAuthorsUseCase = getAllAuthorsUseCase;
        _getAuthorByIdUseCase = getAuthorByIdUseCase;
        _addAuthorUseCase = addAuthorUseCase;
        _updateAuthorUseCase = updateAuthorUseCase;
        _deleteAuthorUseCase = deleteAuthorUseCase;
        _mapper = mapper;
    }


    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<AuthorResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken = default)
    {
        var authors = await _getAllAuthorsUseCase.ExecuteAsync(cancellationToken);
        var response = authors.Select(author => _mapper.Map<AuthorResponse>(author));
        return Ok(response);
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AuthorResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAuthorById(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken);
        var response = _mapper.Map<AuthorResponse>(author);
        return Ok(response);
    }


    [HttpPost("add")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorRequest request, CancellationToken cancellationToken = default)
    {
        var author = new Author(request.Name, request.Surname, request.BirthDate, request.Country);
        await _addAuthorUseCase.ExecuteAsync(author, cancellationToken);
        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }


    [HttpPut("update/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorRequest request, CancellationToken cancellationToken = default)
    {
        var updatedAuthor = _mapper.Map<Author>(request);
        await _updateAuthorUseCase.ExecuteAsync(id, updatedAuthor, cancellationToken);
        return NoContent();
    }


    [HttpDelete("delete/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken = default)
    {
        await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);
        return NoContent();
    }
}