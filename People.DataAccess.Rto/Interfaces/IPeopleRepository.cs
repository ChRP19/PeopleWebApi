using People.DataAccess.Rto.Models;

namespace People.DataAccess.Rto.Interfaces;

public interface IPeopleRepository
{
	Task<List<ChildrenRto>> GetChildrenList(int schoolNumber);
	Task<PersonRto?> GetPerson(int passport);
	Task<IEnumerable<PersonRto?>> GetAllPerson();
	
	Task CreatePerson(PersonRto person);
	Task DeletePerson(int passport);

	Task CreateChildren(ChildrenRto children);
	Task DeleteChildren(int birthСertificate);

	Task CreateToy(ToyRto toy);
	Task DeleteToy(int id);

}