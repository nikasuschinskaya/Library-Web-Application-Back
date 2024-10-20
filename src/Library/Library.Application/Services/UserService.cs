using Library.Application.Interfaces;
using Library.Application.Services.Base;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<User>().GetAll()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public Task NotifyUserAboutBookExpirationAsync(Guid userId, Guid bookId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
