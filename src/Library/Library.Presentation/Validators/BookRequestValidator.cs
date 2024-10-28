using FluentValidation;
using Library.Presentation.Requests;

namespace Library.Presentation.Validators;

public class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator()
    {
        RuleFor(book => book.Name)
            .NotEmpty().WithMessage("Book name is required.")
            .MaximumLength(200).WithMessage("Book name can't be longer than 200 characters.");

        RuleFor(book => book.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^\d{3}-?\d{1,5}-?\d{1,7}-?\d{1,7}-?\d{1}$")
            .WithMessage("Invalid ISBN format. Example: 978-3-16-148410-0.");

        RuleFor(book => book.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description can't be longer than 1000 characters.");

        RuleFor(book => book.GenreId)
            .NotEmpty().WithMessage("GenreId is required.");

        RuleFor(book => book.Count)
            .GreaterThan(0).WithMessage("Count must be greater than zero.");

        RuleFor(book => book.Authors)
            .NotEmpty().WithMessage("At least one author is required.");
    }
}
