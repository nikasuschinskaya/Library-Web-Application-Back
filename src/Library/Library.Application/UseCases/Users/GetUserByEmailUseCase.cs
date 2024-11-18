using Library.Application.Interfaces.UseCases.Users;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Users;

namespace Library.Application.UseCases.Users;

public class GetUserByEmailUseCase : IGetUserByEmailUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByEmailUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<User?> ExecuteAsync(string email, CancellationToken cancellationToken = default)
    {
        var spec = new UserByEmailSpecification(email);
        return await _unitOfWork.Users.GetBySpecAsync(spec, cancellationToken);
    }
}
