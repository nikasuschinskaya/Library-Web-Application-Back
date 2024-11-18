using AutoMapper;
using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Books;

public class UpdateBookUseCase : IUpdateBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Guid bookId, Book updatedBook, CancellationToken cancellationToken = default)
    {
        var existingBook = await _unitOfWork.Books.GetByIdAsync(bookId, cancellationToken)
            ?? throw new EntityNotFoundException($"Book with {bookId} not found.");

        //existingBook.Name = updatedBook.Name;
        //existingBook.ISBN = updatedBook.ISBN;
        //existingBook.GenreId = updatedBook.GenreId;
        //existingBook.Description = updatedBook.Description;
        //existingBook.Count = updatedBook.Count;
        //existingBook.Authors = updatedBook.Authors;

        _mapper.Map(updatedBook, existingBook);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
