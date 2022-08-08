using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Blo.Models;

namespace People.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
	private readonly IPeopleService _service;
	public PeopleController(IPeopleService service)
	{
		_service = service;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<ChildrenBlo>), 200)]
	[ProducesResponseType(404)]
	public Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber, DateTimeOffset dateOfBirth)
	{
		return _service.GetChildrenList(schoolNumber, dateOfBirth);
	}
	
	[HttpGet("/api/v1/Persons/{passport}")]
	public Task<PersonBlo> GetPerson(int passport)
	{
		return _service.GetPerson(passport);
	}

	[HttpPost]
	public async Task<ActionResult<PersonBlo>> PostPerson(PersonBlo person)
	{
		await _service.AddPerson(person);

		return Created("", person);
	}
}