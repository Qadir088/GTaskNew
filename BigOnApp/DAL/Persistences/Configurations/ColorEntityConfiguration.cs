using BigOnApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOnApp.DAL.Persistences.Configurations;

public class ColorEntityConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.Property(m => m.Id).HasColumnType("int");
        builder.Property(m => m.Name).HasColumnType("varchar").HasMaxLength(20).IsRequired();
        builder.Property(m => m.HasCode).HasColumnType("varchar").HasMaxLength(7).IsRequired();

        builder.ConfigureAsAuditable();

        builder.ToTable("Colors");
    }
}
