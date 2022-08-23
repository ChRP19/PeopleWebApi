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
	
	private readonly IValidator<ChildrenBlo> _childrenValidator;
	private readonly IValidator<PersonBlo> _personValidator;
	private readonly IValidator<ToyBlo> _toyValidator;
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
		var result =await _repository.GetPerson(passport);
		return _mapper.Map<PersonRto, PersonBlo>(result);
	}
	public async Task<IEnumerable<PersonBlo?>> GetAllPerson()
	{
		var result = await _repository.GetAllPerson();
		return _mapper.Map<IEnumerable<PersonRto>, IEnumerable<PersonBlo>>(result);
	}
	public async Task CreatePerson(PersonBlo personBlo)
	{
		await _personValidator.ValidateAndThrowAsync(personBlo);
		
		var personRto = _mapper.Map<PersonRto>(personBlo);
		await _repository.CreatePerson(personRto);
	}
	public async Task DeletePerson(int passport)
	{
		await _repository.DeletePerson(passport);
	}
	public async Task CreateChildren(ChildrenBlo childrenBlo)
	{
		await _childrenValidator.ValidateAndThrowAsync(childrenBlo);
		
		var childrenRto = _mapper.Map<ChildrenRto>(childrenBlo);
		await _repository.CreateChildren(childrenRto);
	}
	public async Task DeleteChildren(int birthСertificate)
	{
		await _repository.DeleteChildren(birthСertificate);
	}
	public async Task CreateToy(ToyBlo toyBlo)
	{
		await _toyValidator.ValidateAndThrowAsync(toyBlo);

		var toyRto = _mapper.Map<ToyRto>(toyBlo);
		await _repository.CreateToy(toyRto);
	}
	public async Task DeleteToy(int id)
	{
		await _repository.DeleteToy(id);
	}
}