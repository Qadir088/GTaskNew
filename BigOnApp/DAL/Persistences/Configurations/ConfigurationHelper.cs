using BigOnApp.Models.AudiTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOnApp.DAL.Persistences.Configurations;

public static class ConfigurationHelper
{
    public static EntityTypeBuilder<T> ConfigureAsAuditable<T>(this EntityTypeBuilder<T> builder)
        where T : AudiTableEntity
    {
        builder.Property(m => m.CreatedBy).HasColumnType("int").IsRequired();
        builder.Property(m => m.CreatedAt).HasColumnType("datetime").IsRequired();

        builder.Property(m => m.DeletedBy).HasColumnType("int").IsRequired(false);
        builder.Property(m => m.ModifiedBy).HasColumnType("int").IsRequired(false);
        builder.Property(m => m.DeletedAt).HasColumnType("datetime").IsRequired(false);
        builder.Property(m => m.ModifiedAt).HasColumnType("datetime").IsRequired(false);

        return builder;
    }
}
