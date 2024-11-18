//using Library.Application.Interfaces.Services;
//using Library.Domain.Entities;
//using Library.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Library.Application.Services;

//public class GenreService : IGenreService
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public GenreService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

//    public async Task<IEnumerable<Genre>> GetAllGenresAsync(CancellationToken cancellationToken = default)
//    {
//        return await _unitOfWork.Repository<Genre>().GetAll().ToListAsync(cancellationToken);
//    }

//    public async Task<Genre?> GetGenreByIdAsync(Guid id, CancellationToken cancellationToken = default)
//    {
//        return await _unitOfWork.Repository<Genre>().GetByIdAsync(id, cancellationToken);
//    }
//}
