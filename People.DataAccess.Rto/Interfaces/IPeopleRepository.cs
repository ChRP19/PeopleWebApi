using People.DataAccess.Rto.Models;

namespace People.DataAccess.Rto.Interfaces;

public interface IPeopleRepository
{
	Task<List<ChildrenRto>> GetChildrenList(int schoolNumber);
	Task<PersonRto?> GetPerson(int passport);
	Task<IEnumerable<PersonRto?>> GetAllPerson();
	Task AddPerson(PersonRto person);
	Task DeletePerson(int passport);

}