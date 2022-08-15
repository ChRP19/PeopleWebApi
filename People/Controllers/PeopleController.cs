using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Blo.Models;
using People.DataAccess.Rto.Models;
using People.Models;

namespace People.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
	private readonly IPeopleService _service;
	private readonly IMapper _mapper;
	public PeopleController(IPeopleService service, IMapper mapper)
	{
		_service = service;
		_mapper = mapper;
	}

	[HttpGet("/api/v1/Children")]
	[ProducesResponseType(typeof(List<ChildrenDto>), 200)]
	[ProducesResponseType(404)]
	public async Task<List<ChildrenDto>> GetChildrenList(int schoolNumber)
	{
		var result = _service.GetChildrenList(schoolNumber);
		return _mapper.Map<List<ChildrenBlo>, List<ChildrenDto>>(await result);
	}
	
	[HttpGet("/api/v1/Persons/{passport}")]
	public async Task<PersonDto> GetPerson(int passport)
	{
		var result = _service.GetPerson(passport);
		return _mapper.Map<PersonBlo, PersonDto>(await result);
	}

	[HttpGet("/api/v1/Persons")]
	public async Task<IEnumerable<PersonDto>> GetAllPersons()
	{
		var result = _service.GetAllPerson();
		return _mapper.Map<IEnumerable<PersonBlo>, IEnumerable<PersonDto>>(await result);
	}

	[HttpPost("/api/v1/Persons/Create")]
	public async Task<ActionResult<PersonDto>> CreatePerson(PersonDto person)
	{
		var result = _mapper.Map<PersonBlo>(person);
		await _service.CreatePerson(result);

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
	public async Task<ActionResult<ChildrenDto>> CreateChildren(ChildrenDto children)
	{
		var result = _mapper.Map<ChildrenBlo>(children);
		await _service.CreateChildren(result);

		return Created("", result);
	}

	[HttpDelete("/api/v1/Children/Delete/{birthCertificate}")]
	public async Task<ActionResult> DeleteChildren(int birthCertificate)
	{
		await _service.DeletePerson(birthCertificate);
		return Ok();
	}
	
	[HttpPost("/api/v1/Toys/Create")]
	public async Task<ActionResult<ToyDto>> CreateToy(ToyDto toy)
	{
		var result = _mapper.Map<ToyBlo>(toy);
		await _service.CreateToy(result);

		return Created("", toy);
	}

	[HttpDelete("/api/v1/Toys/Delete/{id}")]
	public async Task<ActionResult> DeleteToy(int id)
	{
		await _service.DeleteToy(id);
		return Ok();
	}
}