using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Blo.Models;
using People.Models;
using ValidationException = FluentValidation.ValidationException;

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
		var result = await _service.GetChildrenList(schoolNumber);
		return _mapper.Map<List<ChildrenBlo>, List<ChildrenDto>>(result);
	}
	
	[HttpGet("/api/v1/Persons/{passport}")]
	public async Task<PersonDto> GetPerson(int passport)
	{
		var result = await _service.GetPerson(passport);
		return _mapper.Map<PersonBlo, PersonDto>(result);
	}

	[HttpGet("/api/v1/Persons")]
	public async Task<IEnumerable<PersonDto>> GetAllPersons()
	{
		var result = await _service.GetAllPerson();
		return _mapper.Map<IEnumerable<PersonBlo>, IEnumerable<PersonDto>>(result);
	}

	[HttpPost("/api/v1/Persons/Create")]
	public async Task<ActionResult<PersonDto>> CreatePerson(PersonDto personDto)
	{
		var personBlo = _mapper.Map<PersonBlo>(personDto);
		try
		{
			await _service.CreatePerson(personBlo);
		}
		catch (ValidationException e)
		{
			return BadRequest(e.Errors.Select(x => x.ErrorMessage));
		}
		
		return CreatedAtAction(nameof(GetPerson), new { passport = personDto.Passport }, personDto);
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
	public async Task<ActionResult<ChildrenDto>> CreateChildren(ChildrenDto childrenDto)
	{
		var childrenBlo = _mapper.Map<ChildrenBlo>(childrenDto);
		try
		{
			await _service.CreateChildren(childrenBlo);
		}
		catch (ValidationException e)
		{
			return BadRequest(e.Errors.Select(x => x.ErrorMessage));
		}

		return Created("", childrenBlo);
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
		try
		{
			await _service.CreateToy(result);
		}
		catch (ValidationException e)
		{
			return BadRequest(e.Errors.Select(x => x.ErrorMessage));
		}

		return Created("", toy);
	}

	[HttpDelete("/api/v1/Toys/Delete/{id}")]
	public async Task<ActionResult> DeleteToy(int id)
	{
		await _service.DeleteToy(id);
		return Ok();
	}
}