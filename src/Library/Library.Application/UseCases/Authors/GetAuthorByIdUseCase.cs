using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Authors;

public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorByIdUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Author?> ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default) =>
        await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken);
}
