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
	public Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber)
	{
		return _service.GetChildrenList(schoolNumber);
	}
	
	[HttpGet("/api/v1/Person/{passport}")]
	public Task<PersonBlo> GetPerson(int passport)
	{
		return _service.GetPerson(passport);
	}

	[HttpGet("/api/v1/Persons")]
	public Task<IEnumerable<PersonBlo>> GetAllPersons()
	{
		return _service.GetAllPerson();
	}

	[HttpPost]
	public async Task<ActionResult<PersonBlo>> PostPerson(PersonBlo person)
	{
		await _service.AddPerson(person);

		return Created("", person);
	}
}