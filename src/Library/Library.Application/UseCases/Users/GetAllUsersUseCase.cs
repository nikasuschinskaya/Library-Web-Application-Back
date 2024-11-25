﻿using Library.Application.Interfaces.UseCases.Users;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Users;

namespace Library.Application.UseCases.Users;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<User>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var spec = new AllUsersOrderedSpecification();
        return await _unitOfWork.Users.ListAsync(spec, cancellationToken);
    }
}