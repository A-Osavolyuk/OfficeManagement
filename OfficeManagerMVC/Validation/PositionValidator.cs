using FluentValidation;
using OfficeManagerMVC.Models.DTOs;

namespace OfficeManagerMVC.Validation
{
    public class PositionValidator : AbstractValidator<PositionDto>
    {
        public PositionValidator()
        {
            RuleFor(x => x.PositionName).NotEmpty().MinimumLength(3).NotEqual("string");
            RuleFor(x => x.DepartmentId).NotEmpty().GreaterThan(0).LessThan(1000);
        }
    }
}
