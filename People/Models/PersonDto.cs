using System;

namespace People.Models;

public class PersonDto
{
	public string Name { get; set; }
	public int? Passport { get; set; }
	public DateTime WeddingDate { get; set; }
	public int? ConvictionsNumber { get; set; }
}