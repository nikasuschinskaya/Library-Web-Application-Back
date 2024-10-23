using AutoMapper;
using Library.Application.Interfaces.Services;
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
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorService authorService, IMapper mapper)
    {
        _authorService = authorService;
        _mapper = mapper;
    }

    
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<AuthorResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken = default)
    {
        var authors = await _authorService.GetAllAuthorsAsync(cancellationToken);
        var response = authors.Select(author => _mapper.Map<AuthorResponse>(author));

        return Ok(response);
    }

    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AuthorResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAuthorById(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _authorService.GetAuthorByIdAsync(id, cancellationToken);
        if (author == null)
            return NotFound("Author not found.");

        var response = _mapper.Map<AuthorResponse>(author);
        return Ok(response);
    }

    
    [HttpGet("{id:guid}/books")]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetBooksByAuthor(Guid id, CancellationToken cancellationToken = default)
    {
        var books = await _authorService.GetBooksByAuthorAsync(id, cancellationToken);
        if (books == null || !books.Any())
            return NotFound("No books found for this author.");

        var response = books.Select(book => _mapper.Map<BookResponse>(book));
        return Ok(response);
    }


    [HttpPost("add")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //var author = _mapper.Map<Author>(request);
        var author = new Author(request.Name, request.Surname, request.BirthDate, request.Country);

        await _authorService.AddAuthorAsync(author, cancellationToken);

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    
    [HttpPut("update/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingAuthor = await _authorService.GetAuthorByIdAsync(id, cancellationToken);
        if (existingAuthor == null)
            return BadRequest("Author not found.");

        var updatedAuthor = _mapper.Map(request, existingAuthor);

        await _authorService.UpdateAuthorAsync(updatedAuthor, cancellationToken);

        return NoContent();
    }

    
    [HttpDelete("delete/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _authorService.GetAuthorByIdAsync(id, cancellationToken);

        if (author == null)
            return BadRequest("Author not found.");

        await _authorService.DeleteAuthorAsync(id, cancellationToken);

        return NoContent();
    }
}