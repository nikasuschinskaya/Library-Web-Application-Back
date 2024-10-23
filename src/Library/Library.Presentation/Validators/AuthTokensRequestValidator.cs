using FluentValidation;
using Library.Presentation.Requests;

namespace Library.Presentation.Validators;

public class AuthTokensRequestValidator : AbstractValidator<AuthTokensRequest>
{
    public AuthTokensRequestValidator()
    {
        RuleFor(tokens => tokens.AccessToken)
            .NotEmpty().WithMessage("Access token is required.");

        RuleFor(tokens => tokens.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}
