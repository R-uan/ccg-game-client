using FluentValidation;
using GameClient.Requests;

namespace GameClient.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(lr => lr.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("That's not an email, that's a war crime");

            RuleFor(lr => lr.Password)
                .NotEmpty().WithMessage("Pasword is required")
                .MinimumLength(8).WithMessage("Password is too short. Stop it.");
        }
    }
}
