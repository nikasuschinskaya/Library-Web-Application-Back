using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class GetBooksPagedUseCase : IGetBooksPagedUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBooksPagedUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PagedList<Book>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken) 
        => await _unitOfWork.Books.ListPagedAsync(new PagedBooksSpecification(), pageNumber, pageSize, cancellationToken);
}
