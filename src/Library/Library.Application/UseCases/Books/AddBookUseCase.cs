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

        foreach (var author in authors)
        {
            book.Authors.Add(author);
        }

        book.Authors.Clear();

        _unitOfWork.Books.Create(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var author in authors)
        {
            _unitOfWork.Authors.Attach(author);

            author.Books.Add(book);

            _unitOfWork.Authors.Update(author);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}