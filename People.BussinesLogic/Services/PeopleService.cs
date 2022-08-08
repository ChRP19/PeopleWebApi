using AutoMapper;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Blo.Models;
using People.DataAccess.Rto.Interfaces;
using People.DataAccess.Rto.Models;

namespace People.BussinesLogic.Services;

public class PeopleService : IPeopleService
{
	private readonly IPeopleRepository _repository;
	private readonly IMapper _mapper;
	public PeopleService(IPeopleRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
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
	public async Task CreatePerson(PersonBlo person)
	{
		var result = _mapper.Map<PersonRto>(person);
		await _repository.AddPerson(result);
	}

}