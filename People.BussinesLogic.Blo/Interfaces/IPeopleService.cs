using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Blo.Interfaces;

public interface IPeopleService
{
	Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber, DateTimeOffset dateOfBirth);
	Task<PersonBlo?> GetPerson(int passport);
	
	Task AddPerson(PersonBlo person);
}