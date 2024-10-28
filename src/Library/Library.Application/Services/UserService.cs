using Library.Application.Interfaces.Common;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default) =>
        await _unitOfWork.Repository<User>().GetAll().ToListAsync(cancellationToken);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<User>().GetAll()
            .Include(u => u.UserBooks)
            .Include(r => r.Role)
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<User>().GetAll()
            .Where(user => user.Id == id)
            .Include(u => u.UserBooks)
            .Include(r => r.Role)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Update(User user, CancellationToken cancellationToken = default)
    {
        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
