using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Authors;

public class UpdateAuthorUseCase : IUpdateAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid authorId, Author updatedAuthor, CancellationToken cancellationToken = default)
    {
        _ = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken)
            ?? throw new EntityNotFoundException($"Author with {authorId} not found.");

        _unitOfWork.Authors.Update(updatedAuthor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
