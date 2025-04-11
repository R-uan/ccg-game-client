using System;
using FluentValidation;
using GameClient.Requests;

namespace GameClient.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(rr => rr.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("That's not an email, that's a war crime");

        RuleFor(rr => rr.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(4).WithMessage("Username too short. Minimum of 4 characteres");

        RuleFor(rr => rr.Password)
            .NotEmpty().WithMessage("Pasword is required")
            .MinimumLength(8).WithMessage("Password is too short. Stop it.");
    }
}
