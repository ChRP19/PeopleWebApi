using AutoMapper;
using FluentValidation;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Blo.Models;
using People.DataAccess.Rto.Interfaces;
using People.DataAccess.Rto.Models;

namespace People.BussinesLogic.Services;

public class PeopleService : IPeopleService
{
	private readonly IPeopleRepository _repository;
	private readonly IMapper _mapper;
	
	private IValidator<ChildrenBlo> _childrenValidator;
	private IValidator<PersonBlo> _personValidator;
	private IValidator<ToyBlo> _toyValidator;
	public PeopleService(IPeopleRepository repository, IMapper mapper, IValidator<ChildrenBlo> childrenValidator, IValidator<PersonBlo> personValidator, IValidator<ToyBlo> toyValidator)
	{
		_repository = repository;
		_mapper = mapper;
		_childrenValidator = childrenValidator;
		_personValidator = personValidator;
		_toyValidator = toyValidator;
	}
	
	public async Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber)
	{
		var result = _repository.GetChildrenList(schoolNumber);
		return _mapper.Map<List<ChildrenRto>, List<ChildrenBlo>>(await result);
	}
	public async Task<PersonBlo?> GetPerson(int passport)
	{
		var result = _repository.GetPerson(passport);
		return _mapper.Map<PersonRto, PersonBlo>(await result);
	}
	public async Task<IEnumerable<PersonBlo?>> GetAllPerson()
	{
		var result = _repository.GetAllPerson();
		return _mapper.Map<IEnumerable<PersonRto>, IEnumerable<PersonBlo>>(await result);
	}
	public async Task<string> CreatePerson(PersonBlo personBlo)
	{
		var result = await _personValidator.ValidateAsync(personBlo);

		if(!result.IsValid)
		{
			return result.Errors.FirstOrDefault()!.ErrorMessage;
		}
		var personRto = _mapper.Map<PersonRto>(personBlo);
		await _repository.CreatePerson(personRto);

		return string.Empty;
	}
	public async Task DeletePerson(int passport)
	{
		await _repository.DeletePerson(passport);
	}
	public async Task<string> CreateChildren(ChildrenBlo childrenBlo)
	{
		var result = await _childrenValidator.ValidateAsync(childrenBlo);

		if(!result.IsValid)
		{
			return result.Errors.FirstOrDefault()!.ErrorMessage;
		}
		var childrenRto = _mapper.Map<ChildrenRto>(childrenBlo);
		await _repository.CreateChildren(childrenRto);
		
		return string.Empty;
	}
	public async Task DeleteChildren(int birthСertificate)
	{
		await _repository.DeleteChildren(birthСertificate);
	}
	public async Task<string> CreateToy(ToyBlo toyBlo)
	{
		var result = await _toyValidator.ValidateAsync(toyBlo);

		if(!result.IsValid)
		{
			return result.Errors.FirstOrDefault()!.ErrorMessage;
		}
		var toyRto = _mapper.Map<ToyRto>(toyBlo);
		await _repository.CreateToy(toyRto);
		
		return string.Empty;
	}
	public async Task DeleteToy(int id)
	{
		await _repository.DeleteToy(id);
	}

}