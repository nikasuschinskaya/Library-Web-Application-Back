using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Authors;

namespace Library.Application.UseCases.Authors;

public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAuthorsUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Author>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        await _unitOfWork.Authors.ListAsync(new AllAuthorsOrderedSpecification(), cancellationToken);
}
