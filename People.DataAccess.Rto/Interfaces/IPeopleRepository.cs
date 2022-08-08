using People.DataAccess.Rto.Models;

namespace People.DataAccess.Rto.Interfaces;

public interface IPeopleRepository
{
	Task<List<ChildrenRto>> GetChildrenList(int schoolNumber, DateTimeOffset dateOfBirth);
	Task<PersonRto?> GetPerson(int passport);

	Task AddPerson(PersonRto person);

}