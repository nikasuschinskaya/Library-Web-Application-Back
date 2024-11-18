using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Authors;

namespace Library.Application.UseCases.Authors;

public class AddAuthorUseCase : IAddAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Author author, CancellationToken cancellationToken = default)
    {
        var spec = new AuthorByDetailsSpecification(author.Name, author.Surname, author.BirthDate);
        var existingAuthor = await _unitOfWork.Authors.GetBySpecAsync(spec, cancellationToken);

        if (existingAuthor != null)
            throw new AlreadyExistsException($"The author {author.Name} {author.Surname} with birrthday of {author.BirthDate} already exists.");

        _unitOfWork.Authors.Create(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
