using FluentValidation;
using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Validations;

public class ToyValidator : AbstractValidator<ToyBlo>
{
	public ToyValidator()
	{
		const string msg = "Error in field {PropertyName}: value {PropertyValue}";
		
		RuleFor(t => t.Name)
			.Length(0, 256)
			.WithMessage(msg)
			.Must(t => t.All(char.IsLetter))
			.WithMessage(msg);
	}
}