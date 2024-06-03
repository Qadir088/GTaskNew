using BigOnApp.Helpers.Services.Interfaces;
using BigOnApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BigOnApp.DAL.context;

public class AppDbContext : DbContext
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IIdentityService _identityService;
    public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeService dateTimeService, IIdentityService identityService) : base(options)
    {
        _dateTimeService = dateTimeService;
        _identityService = identityService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        var changes = this.ChangeTracker.Entries<IAudiTableEntity>();

        if (changes != null)
        {
            foreach (var entity in changes
                .Where(ch => ch.State == EntityState.Added || 
                ch.State == EntityState.Deleted || 
                ch.State == EntityState.Modified))
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                             entity.Entity.CreatedAt = _dateTimeService.ExecutingTime;
                             entity.Entity.CreatedBy = _identityService.GetPrincipialId();
                    break;
                    case EntityState.Deleted:
                            entity.State = EntityState.Modified;
                            entity.Entity.DeletedAt = _dateTimeService.ExecutingTime;
                            entity.Entity.DeletedBy = _identityService.GetPrincipialId();
                    break;
                    case EntityState.Modified:
                        entity.Entity.ModifiedAt = _dateTimeService.ExecutingTime;
                        entity.Entity.ModifiedBy = _identityService.GetPrincipialId();
                    break;
                    default:
                        break;
                }
            }
        }

        return base.SaveChanges();
    }

    public DbSet<Color> Colors { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }


}



//public override int SaveChanges()
//{
//    UpdateAuditFields();
//    return base.SaveChanges();
//}

//public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//{
//    UpdateAuditFields();
//    return base.SaveChangesAsync(cancellationToken);
//}

//private void UpdateAuditFields()
//{
//    var changes = this.ChangeTracker.Entries<IAudiTableEntity>();

//    foreach (var entity in changes)
//    {
//        if (entity.State == EntityState.Added)
//        {
//            entity.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
//            entity.Entity.CreatedBy = 1;
//        }
//        else if (entity.State == EntityState.Modified)
//        {
//            entity.Entity.ModifiedAt = DateTime.UtcNow.AddHours(4);
//            entity.Entity.ModifiedBy = 1;
//        }
//        else if (entity.State == EntityState.Deleted)
//        {
//            entity.State = EntityState.Modified;
//            entity.Entity.DeletedAt = DateTime.UtcNow.AddHours(4);
//            entity.Entity.DeletedBy = 1;
//        }
//    }
//}
