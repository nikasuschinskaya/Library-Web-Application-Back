using Library.Application.Interfaces.UseCases.Users;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Users;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid userId, User user, CancellationToken cancellationToken = default)
    {
        _ = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
            ?? throw new EntityNotFoundException($"User with {userId} not found."); ;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}