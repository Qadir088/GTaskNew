using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BigOnApp.Models;

namespace BigOnApp.DAL.Persistences.Configurations;

public class BrandEntityConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(m => m.Id).HasColumnType("int");
        builder.Property(m => m.Name).HasColumnType("varchar").HasMaxLength(30).IsRequired();
        builder.Property(m => m.ImageUrl).HasColumnType("varchar").HasMaxLength(250).IsRequired();

        builder.ConfigureAsAuditable();

        builder.ToTable("Brands");
    }
}
