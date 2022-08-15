﻿using System;
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

	[HttpGet("/api/v1/Children")]
	[ProducesResponseType(typeof(List<ChildrenBlo>), 200)]
	[ProducesResponseType(404)]
	public Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber)
	{
		return _service.GetChildrenList(schoolNumber);
	}
	
	[HttpGet("/api/v1/Persons/{passport}")]
	public async Task<PersonBlo> GetPerson(int passport)
	{
		return await _service.GetPerson(passport);
	}

	[HttpGet("/api/v1/Persons")]
	public async Task<IEnumerable<PersonBlo>> GetAllPersons()
	{
		return await _service.GetAllPerson();
	}

	[HttpPost("/api/v1/Persons/Create")]
	public async Task<ActionResult<PersonBlo>> CreatePerson(PersonBlo person)
	{
		await _service.CreatePerson(person);

		return CreatedAtAction(nameof(GetPerson), new { passport = person.Passport }, person);
	}

	[HttpDelete("/api/v1/Persons/Delete/{passport}")]
	public async Task<ActionResult> DeletePerson(int passport)
	{
		var person = await _service.GetPerson(passport);

		if (person is not null)
		{
			await _service.DeletePerson(passport);
			return Ok();
		}
		return NotFound();
	}
	
	[HttpPost("/api/v1/Children/Create")]
	public async Task<ActionResult<PersonBlo>> CreateChildren(ChildrenBlo children)
	{
		await _service.CreateChildren(children);

		return Created("", children);
	}

	[HttpDelete("/api/v1/Children/Delete/{birthCertificate}")]
	public async Task<ActionResult> DeleteChildren(int birthCertificate)
	{
		await _service.DeletePerson(birthCertificate);
		return Ok();
	}
	
	[HttpPost("/api/v1/Toys/Create")]
	public async Task<ActionResult<ToyBlo>> CreateToy(ToyBlo toy)
	{
		await _service.CreateToy(toy);

		return Created("", toy);
	}

	[HttpDelete("/api/v1/Toys/Delete/{id}")]
	public async Task<ActionResult> DeleteToy(int id)
	{
		await _service.DeleteToy(id);
		return Ok();
	}
}