using Microsoft.EntityFrameworkCore;
using People.DataAccess.Contexts;
using People.DataAccess.Rto.Interfaces;
using People.DataAccess.Rto.Models;

namespace People.DataAccess.Repositories;

public class PeopleRepository : IPeopleRepository
{
	private readonly SqlPeopleContext _context;
	public PeopleRepository(SqlPeopleContext context)
	{
		_context = context;
	}
	
	public async Task<List<ChildrenRto>> GetChildrenList(int schoolNumber, DateTimeOffset dateOfBirth)
	{
		var result = _context.Childrens
			.AsNoTracking()
			.Where(c => c.SchoolNumber == schoolNumber && c.DateOfBirth == dateOfBirth)
			.ToList();
		return result;
	}
	public async Task<PersonRto?> GetPerson(int passport)
	{
		var result = _context.Persons
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Passport == passport);
		return await result;
	}
	public async Task AddPerson(PersonRto person)
	{
		_context.Persons.Add(person);
		await _context.SaveChangesAsync();
	}
	
}