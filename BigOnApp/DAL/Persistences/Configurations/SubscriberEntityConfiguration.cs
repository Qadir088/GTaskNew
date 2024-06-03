using BigOnApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOnApp.DAL.Persistences.Configurations;

public class SubscriberEntityConfiguration : IEntityTypeConfiguration<Subscriber>
{
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
        builder.Property(s => s.EmailAdreess).HasColumnType("varchar").HasMaxLength(128);
        builder.Property(s => s.IsAprovved).HasColumnType("bit");
        builder.Property(s => s.CreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(s => s.AprovvedAt).HasColumnType("datetime");
        builder.HasKey(s => s.EmailAdreess);
        builder.ToTable("Subscribers");
    }
}
