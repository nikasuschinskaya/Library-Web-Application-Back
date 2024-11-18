using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class AddBookUseCase : IAddBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Book book, IEnumerable<Guid> authorIds, CancellationToken cancellationToken = default)
    {
        var existingBook = await _unitOfWork.Books.GetBySpecAsync(new BookByISBNSpecification(book.ISBN), cancellationToken);

        if (existingBook != null)
            throw new AlreadyExistsException($"The book with ISBN {book.ISBN} already exists.");

        var authors = new List<Author>();

        foreach (var authorId in authorIds)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken)
                ?? throw new EntityNotFoundException($"Author with ID {authorId} was not found.");
            authors.Add(author);
        }

        book.Authors = authors;

        book.Authors.Clear();

        _unitOfWork.Books.Create(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var author in authors)
        {
            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(author.Id, cancellationToken)
                ?? throw new EntityNotFoundException($"Author with ID {author.Id} was not found.");

            existingAuthor.Books.Add(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }


    //var existingBook = await _unitOfWork.Books.GetBySpecAsync(new BookByISBNSpecification(_book.ISBN), cancellationToken);

    //if (existingBook != null)
    //    throw new AlreadyExistsException($"The book with ISBN {_book.ISBN} already exists.");

    //var authors = new List<Author>();

    //foreach (var authorId in _authorIds)
    //{
    //    var author = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken)
    //        ?? throw new EntityNotFoundException($"Author with ID {authorId} was not found.");
    //    authors.Add(author);
    //}

    //_book.Authors = authors;
    //_unitOfWork.Books.Create(_book);
    //await _unitOfWork.SaveChangesAsync(cancellationToken);


}
