using FluentValidation;
using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Validations;

public class ChildrenValidator : AbstractValidator<ChildrenBlo>
{
	public ChildrenValidator()
	{
		const string msg = "Error in field {PropertyName}: value {PropertyValue}";

		RuleFor(c => c.Name)
			.Length(0, 256).WithMessage(msg)
			.Must(c => c.All(char.IsLetter)).WithMessage(msg);
		RuleFor(c => c.SchoolNumber).GreaterThan(0).WithMessage(msg);
	}
}