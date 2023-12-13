using FluentValidation;
using OfficeManagerBlazorServer.Models.DTOs;

namespace OfficeManagerBlazorServer.Validation
{
    public class DepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Department Name cannot be empty.")
                .MinimumLength(2).WithMessage("Department Name must contain at least 2 characters.");
        }
    }
}
