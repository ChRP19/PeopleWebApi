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
		var result = await _context.Persons
			.AsNoTracking()
			.SingleOrDefaultAsync(p => p.Passport == passport);
		return result;
	}
	public async Task<IEnumerable<PersonRto?>> GetAllPerson()
	{
		return _context.Persons
			.AsNoTracking()
			.ToList();
	}
	public async Task CreatePerson(PersonRto person)
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
	public async Task CreateChildren(ChildrenRto children)
	{
		_context.Children.Add(children);
		await _context.SaveChangesAsync();
	}
	public async Task DeleteChildren(int birthСertificate)
	{
		var children = await _context.Children
			.AsNoTracking()
			.SingleOrDefaultAsync(c => c.BirthСertificate == birthСertificate);
		if (children is null) return;
		_context.Children.Remove(children);
		await _context.SaveChangesAsync();
	}
	public async Task CreateToy(ToyRto toy)
	{
		_context.Toys.Add(toy);
		await _context.SaveChangesAsync();
	}
	public async Task DeleteToy(int id)
	{
		var toy = await _context.Toys.FindAsync(id);
		if (toy is null) return;
		_context.Toys.Remove(toy);
		await _context.SaveChangesAsync();
	}

}