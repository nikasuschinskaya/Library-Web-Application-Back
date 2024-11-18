using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Authors;

public class DeleteAuthorUseCase : IDeleteAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken)
            ?? throw new EntityNotFoundException($"Author with ID {authorId} was not found.");

        _unitOfWork.Authors.Delete(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}