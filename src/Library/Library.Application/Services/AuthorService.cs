//using Library.Application.Interfaces.Services;
//using Library.Domain.Entities;
//using Library.Domain.Exceptions;
//using Library.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Library.Application.Services;

//public class AuthorService : IAuthorService
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public AuthorService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

//    public async Task AddAuthorAsync(Author author, CancellationToken cancellationToken = default)
//    {
//        var existingAuthor = await _unitOfWork.Repository<Author>().GetAll()
//            .AnyAsync(a =>
//                a.Name == author.Name &&
//                a.Surname == author.Surname &&
//                a.BirthDate == author.BirthDate, 
//                cancellationToken);

//        if (existingAuthor) throw new AlreadyExistsException();

//        _unitOfWork.Repository<Author>().Create(author); 
//        await _unitOfWork.CompleteAsync(cancellationToken);
//    }

//    public async Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default)
//    {
//        var author = await GetAuthorByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException();
//        _unitOfWork.Repository<Author>().Delete(author);
//        await _unitOfWork.CompleteAsync(cancellationToken);
//    }

//    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)
//    {
//        return await _unitOfWork.Repository<Author>().GetAll().ToListAsync(cancellationToken);
//    }

//    public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken = default)
//    {
//        return await _unitOfWork.Repository<Author>().GetByIdAsync(id, cancellationToken);
//    }

//    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default)
//    {
//        return await _unitOfWork.Repository<Book>().GetAll()
//            .Where(book => book.Authors.Any(author => author.Id == authorId))
//            .ToListAsync(cancellationToken);
//    }

//    public async Task UpdateAuthorAsync(Author author, CancellationToken cancellationToken = default)
//    {
//        _unitOfWork.Repository<Author>().Update(author);
//        await _unitOfWork.CompleteAsync(cancellationToken);
//    }
//}
