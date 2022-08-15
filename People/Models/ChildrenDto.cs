using System;

namespace People.Models;

public class ChildrenDto
{
	public string Name { get; set; }
	public int? BirthСertificate { get; set; }
	public DateTimeOffset DateOfBirth { get; set; }
	public int? SchoolNumber { get; set; }
}