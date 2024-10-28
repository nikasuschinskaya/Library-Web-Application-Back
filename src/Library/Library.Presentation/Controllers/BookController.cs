using AutoMapper;
using Library.Application.Interfaces.Services;
using Library.Application.Models;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Authorization;
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


    [HttpPost("add")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBook([FromBody] BookRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var book = _mapper.Map<Book>(request);

        await _bookService.AddBookAsync(book, cancellationToken);

        return CreatedAtAction(nameof(GetBookInfo), new { book.ISBN }, book);
    }


    [HttpPut("update/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedBook = _mapper.Map<Book>(request);

        await _bookService.UpdateBookAsync(id, updatedBook, cancellationToken);

        return NoContent();
    }


    [HttpDelete("delete/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _bookService.GetBookByIdAsync(id, cancellationToken);

        if (book == null)
            return BadRequest("Book not found.");

        await _bookService.DeleteBookAsync(id, cancellationToken);

        return NoContent();
    }


    [HttpPut("add-image/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBookImage(Guid id, [FromBody] string imageUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return BadRequest("Image URL is required.");

        await _bookService.AddBookImageAsync(id, imageUrl, cancellationToken);

        return NoContent();
    }


    [HttpGet("all/{pageNumber:int}")]
    [ProducesResponseType(typeof(PagedList<BookListResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllBooksOnPage(int pageNumber, CancellationToken cancellationToken = default)
    {
        const int pageSize = 9;

        var pagedBooks = await _bookService.GetBooksPagedAsync(pageNumber, pageSize, cancellationToken);

        if (pagedBooks == null || pagedBooks.Items.Count == 0)
            return BadRequest("No books found on the requested page");

        var response = pagedBooks.Map(book => _mapper.Map<BookListResponse>(book));

        return Ok(response);
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _bookService.GetBookByIdAsync(id, cancellationToken);
        if (book == null)
            return NotFound("Book not found.");

        var response = _mapper.Map<BookResponse>(book);
        return Ok(response);
    }


    [HttpGet("book-info/{ISBN}")]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookInfo(string ISBN, CancellationToken cancellationToken = default)
    {
        var book = await _bookService.GetBookByISBNAsync(ISBN, cancellationToken);
        var response = _mapper.Map<Book, BookResponse>(book);
        return book is not null ? Ok(response) : BadRequest();
    }


    [HttpGet("search/{title}")]
    [ProducesResponseType(typeof(IEnumerable<BookListResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SearchBooksByTitle(string title, CancellationToken cancellationToken = default)
    {
        var books = await _bookService.SearchBooksByTitleAsync(title, cancellationToken);

        var response = _mapper.Map<IEnumerable<BookListResponse>>(books ?? Enumerable.Empty<Book>());

        return Ok(response);
    }

    [HttpGet("filter")]
    [ProducesResponseType(typeof(IEnumerable<BookListResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> FilterBooks([FromQuery] FilterBookRequest request, CancellationToken cancellationToken = default)
    {
        var books = await _bookService.FilterBooksAsync(request.Genre, request.AuthorName, cancellationToken);

        var response = _mapper.Map<IEnumerable<BookListResponse>>(books ?? Enumerable.Empty<Book>());

        return Ok(response);
    }


    [HttpPut("return-book")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> ReturnBook([FromBody] UserBookRequest request, CancellationToken cancellationToken = default)
    {
        await _bookService.ReturnBookAsync(request.BookId, request.UsedId, cancellationToken);
        return NoContent();
    }


    [HttpPut("take-book")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> TakeBook([FromBody] UserBookRequest request, CancellationToken cancellationToken = default)
    {
        await _bookService.TakeBookAsync(request.BookId, request.UsedId, cancellationToken);
        return NoContent();
    }
}
