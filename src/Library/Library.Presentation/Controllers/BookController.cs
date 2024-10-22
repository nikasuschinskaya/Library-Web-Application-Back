using AutoMapper;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    //[ProducesResponseType(typeof(BookListResponse), (int)HttpStatusCode.OK)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var books = await _bookService.GetAllBooksAsync(cancellationToken);
        var response = _mapper.Map<IEnumerable<Book>, IEnumerable<BookListResponse>>(books);
        return Ok(response);
    }

    [HttpPut("return-book")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> ReturnBook(UserBookRequest request, CancellationToken cancellationToken = default)
    {
        await _bookService.ReturnBookAsync(request.BookId, request.UsedId, cancellationToken);
        return NoContent();
    }

    [HttpPut("take-book")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> TakeBook(UserBookRequest request, CancellationToken cancellationToken = default)
    {
        await _bookService.TakeBookAsync(request.BookId, request.UsedId, cancellationToken);
        return NoContent();
    }
}
