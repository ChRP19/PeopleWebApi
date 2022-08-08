using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.DataAccess.Rto.Models;

namespace People.DataAccess.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<PersonRto>
{
	public void Configure(EntityTypeBuilder<PersonRto> builder)
	{
		builder.ToTable("Persons");

		builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
		builder.Property(p => p.WeddingDate).IsRequired();
	}
}