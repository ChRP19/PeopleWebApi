using System.Linq;
using FluentValidation;
using People.Models;

namespace People.Validations;

public class ToyValidator : AbstractValidator<ToyDto>
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