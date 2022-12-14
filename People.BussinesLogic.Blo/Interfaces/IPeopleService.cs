using People.BussinesLogic.Blo.Models;

namespace People.BussinesLogic.Blo.Interfaces;

public interface IPeopleService
{
	Task<List<ChildrenBlo>> GetChildrenList(int schoolNumber);
	Task<PersonBlo?> GetPerson(int passport);
	Task<IEnumerable<PersonBlo?>> GetAllPerson();
	
	Task CreatePerson(PersonBlo person);
	Task DeletePerson(int passport);
	
	Task CreateChildren(ChildrenBlo children);
	Task DeleteChildren(int birthСertificate);

	Task CreateToy(ToyBlo toy);
	Task DeleteToy(int id);
}