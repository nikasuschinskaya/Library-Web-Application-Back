using FluentValidation;
using Library.Presentation.Requests;

namespace Library.Presentation.Validators;

public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
{
    public AuthorRequestValidator()
    {
        RuleFor(author => author.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name can't be longer than 100 characters.");

        RuleFor(author => author.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(100).WithMessage("Surname can't be longer than 100 characters.");

        RuleFor(author => author.BirthDate)
            .LessThan(DateTime.Today).WithMessage("BirthDate must be in the past.");

        RuleFor(author => author.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country can't be longer than 100 characters.");
    }
}
