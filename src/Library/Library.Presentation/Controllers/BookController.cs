using AutoMapper;
using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Models;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    //private readonly IBookService _bookService;
    private readonly IAddBookUseCase _addBookUseCase;
    private readonly IAddBookImageUseCase _addBookImageUseCase;
    private readonly IUpdateBookUseCase _updateBookUseCase;
    private readonly IGetBookByIdUseCase _getBookByIdUseCase;
    private readonly IDeleteBookUseCase _deleteBookUseCase;
    private readonly IGetBookByISBNUseCase _getBookByISBNUseCase;
    private readonly IGetBooksPagedUseCase _getBooksPagedUseCase;
    private readonly ISearchBooksByTitleUseCase _searchBooksByTitleUseCase;
    private readonly ITakeBookUseCase _takeBookUseCase;
    private readonly IFilterBooksUseCase _filterBooksUseCase;
    private readonly IReturnBookUseCase _returnBookUseCase;
    private readonly IMapper _mapper;

    public BookController(IAddBookUseCase addBookUseCase,
                          IAddBookImageUseCase addBookImageUseCase,
                          IUpdateBookUseCase updateBookUseCase,
                          IGetBookByIdUseCase getBookByIdUseCase,
                          IDeleteBookUseCase deleteBookUseCase,
                          IGetBookByISBNUseCase getBookByISBNUseCase,
                          IGetBooksPagedUseCase getBooksPagedUseCase,
                          ISearchBooksByTitleUseCase searchBooksByTitleUseCase,
                          ITakeBookUseCase takeBookUseCase,
                          IFilterBooksUseCase filterBooksUseCase,
                          IReturnBookUseCase returnBookUseCase,
                          IMapper mapper)
    {
        _addBookUseCase = addBookUseCase;
        _addBookImageUseCase = addBookImageUseCase;
        _updateBookUseCase = updateBookUseCase;
        _getBookByIdUseCase = getBookByIdUseCase;
        _deleteBookUseCase = deleteBookUseCase;
        _getBookByISBNUseCase = getBookByISBNUseCase;
        _getBooksPagedUseCase = getBooksPagedUseCase;
        _searchBooksByTitleUseCase = searchBooksByTitleUseCase;
        _takeBookUseCase = takeBookUseCase;
        _filterBooksUseCase = filterBooksUseCase;
        _returnBookUseCase = returnBookUseCase;
        _mapper = mapper;
    }



    //public BookController(IBookService bookService, IMapper mapper)
    //{
    //    _bookService = bookService;
    //    _mapper = mapper;
    //}


    [HttpPost("add")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddBook([FromBody] BookAddRequest request, CancellationToken cancellationToken = default)
    {
        var book = _mapper.Map<Book>(request);
        //await _bookService.AddBookAsync(book, request.AuthorIds, cancellationToken);
        await _addBookUseCase.ExecuteAsync(book, request.AuthorIds, cancellationToken);
        return CreatedAtAction(nameof(GetBookInfo), new { book.ISBN }, book);
    }


    //[HttpPost("add")]
    //[ProducesResponseType((int)HttpStatusCode.Created)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    ////[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> AddBook([FromBody] BookRequest request, CancellationToken cancellationToken = default)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    var book = _mapper.Map<Book>(request);

    //    await _bookService.AddBookAsync(book, cancellationToken);

    //    return CreatedAtAction(nameof(GetBookInfo), new { book.ISBN }, book);
    //}


    [HttpPut("update/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookRequest request, CancellationToken cancellationToken = default)
    {
        var updatedBook = _mapper.Map<Book>(request);
        //await _bookService.UpdateBookAsync(id, updatedBook, cancellationToken);
        await _updateBookUseCase.ExecuteAsync(id, updatedBook, cancellationToken);
        return NoContent();
    }


    [HttpDelete("delete/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken = default)
    {
        //var book = await _bookService.GetBookByIdAsync(id, cancellationToken);

        //if (book == null)
        //    return BadRequest("Book not found.");

        //await _bookService.DeleteBookAsync(id, cancellationToken);
        await _deleteBookUseCase.ExecuteAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpPut("add-image/{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBookImage(Guid id, [FromBody] string imageUrl, CancellationToken cancellationToken = default)
    {
        //await _bookService.AddBookImageAsync(id, imageUrl, cancellationToken);
        await _addBookImageUseCase.ExecuteAsync(id, imageUrl, cancellationToken);
        return NoContent();
    }


    [HttpGet("all/{pageNumber:int}")]
    [ProducesResponseType(typeof(PagedList<BookListResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllBooksOnPage(int pageNumber, CancellationToken cancellationToken = default)
    {
        const int pageSize = 9;
        //var pagedBooks = await _bookService.GetBooksPagedAsync(pageNumber, pageSize, cancellationToken);
        var pagedBooks = await _getBooksPagedUseCase.ExecuteAsync(pageNumber, pageSize, cancellationToken);
        var response = pagedBooks.Map(book => _mapper.Map<BookListResponse>(book));
        return Ok(response);
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken = default)
    {
        //var book = await _bookService.GetBookByIdAsync(id, cancellationToken);
        var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);
        var response = _mapper.Map<BookResponse>(book);
        return Ok(response);
    }


    [HttpGet("book-info/{ISBN}")]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookInfo(string ISBN, CancellationToken cancellationToken = default)
    {
        //var book = await _bookService.GetBookByISBNAsync(ISBN, cancellationToken);
        var book = await _getBookByISBNUseCase.ExecuteAsync(ISBN, cancellationToken);
        var response = _mapper.Map<Book, BookResponse>(book);
        return Ok(response);
    }


    [HttpGet("search/{title}")]
    [ProducesResponseType(typeof(IEnumerable<BookListResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SearchBooksByTitle(string title, CancellationToken cancellationToken = default)
    {
        //var books = await _bookService.SearchBooksByTitleAsync(title, cancellationToken);
        var books = await _searchBooksByTitleUseCase.ExecuteAsync(title, cancellationToken);
        var response = _mapper.Map<IEnumerable<BookListResponse>>(books ?? Enumerable.Empty<Book>());
        return Ok(response);
    }

    [HttpGet("filter")]
    [ProducesResponseType(typeof(IEnumerable<BookListResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> FilterBooks([FromQuery] FilterBookRequest request, CancellationToken cancellationToken = default)
    {
        //var books = await _bookService.FilterBooksAsync(request.Genre, request.AuthorName, cancellationToken);
        var books = await _filterBooksUseCase.ExecuteAsync(request.Genre, request.AuthorName, cancellationToken);
        var response = _mapper.Map<IEnumerable<BookListResponse>>(books ?? Enumerable.Empty<Book>());
        return Ok(response);
    }


    [HttpPut("return-book")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> ReturnBook([FromBody] UserBookRequest request, CancellationToken cancellationToken = default)
    {
        //await _bookService.ReturnBookAsync(request.BookId, request.UsedId, cancellationToken);
        await _returnBookUseCase.ExecuteAsync(request.BookId, request.UsedId, cancellationToken);
        return NoContent();
    }


    [HttpPut("take-book")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> TakeBook([FromBody] UserBookRequest request, CancellationToken cancellationToken = default)
    {
        //await _bookService.TakeBookAsync(request.BookId, request.UsedId, cancellationToken);
        await _takeBookUseCase.ExecuteAsync(request.BookId, request.UsedId, cancellationToken);
        return NoContent();
    }
}
