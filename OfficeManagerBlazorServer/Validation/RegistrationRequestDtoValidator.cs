using FluentValidation;
using OfficeManagerBlazorServer.Models.DTOs;
using System.Text.RegularExpressions;

namespace OfficeManagerBlazorServer.Validation
{
    public class RegistrationRequestDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Field 'User Name' is must.")
                .MinimumLength(2).WithMessage("Must contain at least 2 characters.")
                .NotEqual("string");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Field 'Email' is must.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Field 'Phone Number' is must.")
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(new Regex(@"^(\+\d{12}|\d{10})$")).WithMessage("PhoneNumber is not valid");
            RuleFor(p => p.Password)
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
