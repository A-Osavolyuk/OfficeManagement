using DepartmentsAPI.Models.DTOs;
using FluentValidation;

namespace DepartmentsAPI.Validation
{
    public class DepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().NotEqual("string").MinimumLength(2);
        }
    }
}
