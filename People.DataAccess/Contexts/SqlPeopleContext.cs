using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using People.DataAccess.Configurations;
using People.DataAccess.Rto.Models;

namespace People.DataAccess.Contexts;

public class SqlPeopleContext : DbContext
{
	public DbSet<PersonRto> Persons { get; set; }
	public DbSet<ChildrenRto> Children { get; set; }
	public DbSet<ToyRto> Toys { get; set; }

	public SqlPeopleContext(DbContextOptions<SqlPeopleContext> options)
		: base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}