using FluentValidation;
using OfficeManagerMVC.Models.ViewModels;

namespace OfficeManagerMVC.Validation
{
    public class PositionValidator : AbstractValidator<CreatePositionViewModel>
    {
        public PositionValidator()
        {
            RuleFor(x => x.PositionName).NotEmpty().MinimumLength(3).NotEqual("string");
            RuleFor(x => x.DepartmentId).NotEmpty().GreaterThan(0).LessThan(1000);
        }
    }
}
