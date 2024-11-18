using Library.Application.Interfaces.UseCases.Users;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Users;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<User?> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
}
