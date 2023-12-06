using FluentValidation;
using PositionsAPI.Models.DTOs;

namespace PositionsAPI.Validation
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
