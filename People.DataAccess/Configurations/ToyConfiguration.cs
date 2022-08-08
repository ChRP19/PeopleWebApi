using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.DataAccess.Rto.Models;

namespace People.DataAccess.Configurations;

public class ToyConfiguration : IEntityTypeConfiguration<ToyRto>
{
	public void Configure(EntityTypeBuilder<ToyRto> builder)
	{
		builder.ToTable("Toys");

		builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
		builder.Property(t => t.Manufacturer).IsRequired();
		builder.Property(t => t.CountryOfManufacture).IsRequired();

		builder.HasOne(t => t.ChildrenRto)
			.WithMany(c => c.Toys)
			.HasForeignKey(t => t.ChildrenId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}