using FluentValidation;
using Library.Presentation.Requests;

namespace Library.Presentation.Validators;

public class UserBookRequestValidator : AbstractValidator<UserBookRequest>
{
    public UserBookRequestValidator()
    {
        RuleFor(request => request.UsedId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(request => request.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}
