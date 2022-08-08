using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Blo.Interfaces;

public interface IPeopleService
{
	Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber);
	Task<PersonBlo?> GetPerson(int passport);
	Task<IEnumerable<PersonBlo?>> GetAllPerson();
	Task AddPerson(PersonBlo person);
}