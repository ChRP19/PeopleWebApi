using FluentValidation;
using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Validators;

public class ToyValidator : AbstractValidator<ToyBlo>
{
	public ToyValidator()
	{
		const string msg = "Error in field {PropertyName}: value {PropertyValue}";
		
		RuleFor(t => t.Name)
			.Length(1, 256)
			.WithMessage(msg)
			.Must(t => t.All(char.IsLetter))
			.WithMessage(msg);
	}
}