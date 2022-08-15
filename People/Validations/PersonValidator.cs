using System.Linq;
using FluentValidation;
using People.Models;

namespace People.Validations;

public class PersonValidator : AbstractValidator<PersonDto>
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