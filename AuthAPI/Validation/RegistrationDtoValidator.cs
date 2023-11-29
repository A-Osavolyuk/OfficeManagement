using AuthAPI.Models.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AuthAPI.Validation
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(2).NotEqual("string");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull()
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(new Regex(@"^(\+\d{12}|\d{10})$")).WithMessage("PhoneNumber is not valid");
            RuleFor(p => p.Password).Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
