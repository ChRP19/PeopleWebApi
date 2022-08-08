using Microsoft.EntityFrameworkCore;
using People.DataAccess.Contexts;
using People.DataAccess.Rto.Interfaces;
using People.DataAccess.Rto.Models;

namespace People.DataAccess.Repositories;

public class PeopleRepository : IPeopleRepository
{
	private readonly SqlPeopleContext _context;
	public PeopleRepository(SqlPeopleContext context) =>
		_context = context;

	public async Task<List<ChildrenRto>> GetChildrenList(int schoolNumber)
	{
		var result = _context.Children
			.AsNoTracking()
			.Where(c => c.SchoolNumber == schoolNumber)
			.ToList();
		return result;
	}
	public async Task<PersonRto?> GetPerson(int passport)
	{
		var result = _context.Persons
			.AsNoTracking()
			.SingleOrDefaultAsync(p => p.Passport == passport);
		return await result;
	}
	public async Task<IEnumerable<PersonRto?>> GetAllPerson()
	{
		return _context.Persons
			.AsNoTracking()
			.ToList();
	}
	public async Task AddPerson(PersonRto person)
	{
		_context.Persons.Add(person);
		await _context.SaveChangesAsync();
	}
	public async Task DeletePerson(int passport)
	{
		var person = await _context.Persons
			.AsNoTracking()
			.SingleOrDefaultAsync(p => p.Passport == passport);
		if (person is null) return;
		_context.Persons.Remove(person);
		await _context.SaveChangesAsync();
	}

}