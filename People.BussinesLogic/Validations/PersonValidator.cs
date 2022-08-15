using FluentValidation;
using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Validations;

public class PersonValidator : AbstractValidator<PersonBlo>
{
	public PersonValidator()
	{
		const string msg = "Error in field {PropertyName}: value {PropertyValue}";
		
		RuleFor(p => p.Name)
			.Length(0, 256)
			.WithMessage(msg)
			.Must(p => p.All(char.IsLetter))
			.WithMessage(msg);
		RuleFor(p => p.Passport)
			.GreaterThan(100000)
			.WithMessage(msg)
			.LessThan(999999)
			.WithMessage(msg);
	}
}