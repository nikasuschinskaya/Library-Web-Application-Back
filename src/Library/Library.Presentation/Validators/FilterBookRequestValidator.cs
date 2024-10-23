using FluentValidation;
using Library.Presentation.Requests;

namespace Library.Presentation.Validators;

public class FilterBookRequestValidator : AbstractValidator<FilterBookRequest>
{
    public FilterBookRequestValidator()
    {
        RuleFor(request => request.Genre)
            .MaximumLength(100).WithMessage("Genre can't be longer than 100 characters.")
            .When(request => !string.IsNullOrEmpty(request.Genre));

        RuleFor(request => request.AuthorName)
            .MaximumLength(200).WithMessage("Author name can't be longer than 200 characters.")
            .When(request => !string.IsNullOrEmpty(request.AuthorName));
    }
}
