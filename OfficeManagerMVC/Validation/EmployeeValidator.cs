using FluentValidation;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.ViewModels;
using System.Text.RegularExpressions;

namespace OfficeManagerMVC.Validation
{
    public class EmployeeValidator : AbstractValidator<CreateEmployeeViewModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).MinimumLength(10).MaximumLength(20)
                .Matches(new Regex(@"^(\+\d{12}|\d{10})$")).WithMessage("PhoneNumber is not valid");
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(32).NotEqual("string");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(32).NotEqual("string");
            RuleFor(x => x.Gender).NotEmpty().MinimumLength(2).MaximumLength(32).NotEqual("string");
            RuleFor(x => x.DateOfBirth).NotEmpty().NotNull().LessThan(DateTime.UtcNow);
            RuleFor(x => x.DateOfHire).NotEmpty().NotNull().LessThan(DateTime.UtcNow.AddSeconds(1));
            RuleFor(x => x.PositionId).LessThan(1000).GreaterThan(0);
        }
    }
}
