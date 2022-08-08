using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using People.DataAccess.Rto.Models;

namespace People.DataAccess.Configurations;

public class ChildrenConfiguration : IEntityTypeConfiguration<ChildrenRto>
{
	public void Configure(EntityTypeBuilder<ChildrenRto> builder)
	{
		builder.ToTable("Childrens");

		builder.Property(с => с.Name).HasMaxLength(100).IsRequired();
		builder.Property(с => с.DateOfBirth).IsRequired();

		builder.HasOne(c => c.PersonRto)
			.WithMany(p => p.Childrens)
			.HasForeignKey(c => c.PersonId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}