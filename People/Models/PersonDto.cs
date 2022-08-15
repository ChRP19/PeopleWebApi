using System;

namespace People.Models;

public class PersonDto
{
	public string Name { get; set; }
	public int? Passport { get; set; }
	public DateTimeOffset WeddingDate { get; set; }
	public int? ConvictionsNumber { get; set; }
}