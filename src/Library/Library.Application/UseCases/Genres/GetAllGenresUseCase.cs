using Library.Application.Interfaces.UseCases.Genres;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Genres;

namespace Library.Application.UseCases.Genres;

public class GetAllGenresUseCase : IGetAllGenresUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGenresUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Genre>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var spec = new AllGenresOrderedSpecification();
        return await _unitOfWork.Genres.ListAsync(spec, cancellationToken);
    }
}
