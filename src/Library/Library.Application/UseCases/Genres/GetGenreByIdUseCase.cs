using Library.Application.Interfaces.UseCases.Genres;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Genres;

public class GetGenreByIdUseCase : IGetGenreByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGenreByIdUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Genre?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _unitOfWork.Genres.GetByIdAsync(id, cancellationToken);
}
