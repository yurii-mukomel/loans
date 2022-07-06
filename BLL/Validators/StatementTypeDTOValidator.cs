using BLL.DTO;
using FluentValidation;

namespace BLL.Validators
{
    public class StatementTypeDTOValidator : AbstractValidator<StatementTypeDTO>
    {
        public StatementTypeDTOValidator()
        {
            RuleFor(st => st.Name).NotNull().NotEmpty();
            RuleFor(st => st.Percentage).NotEmpty().InclusiveBetween((short)0, (short)100);
            RuleFor(st => st.MinTerm).GreaterThan(0);
            RuleFor(st => st.MaxTerm).GreaterThan(s => s.MinTerm);
            RuleFor(st => st.MinAmount).GreaterThan(0);
            RuleFor(st => st.MaxAmount).GreaterThan(s => s.MinAmount);
        }
    }
}
