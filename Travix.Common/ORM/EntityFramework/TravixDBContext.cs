using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Travix.Common.Models;
using Travix.Common.ORM.Models;

namespace Travix.Common.ORM.EntityFramework;

public class TravixDBContext : DbContext
{

    public RequestContext RequestContext { get; set; }
    

    public TravixDBContext(DbContextOptions options, RequestContext requestContext) : base(options)
    {
        RequestContext = requestContext;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
    
    public override int SaveChanges()
    {
        ApplyEkycConcepts();
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyEkycConcepts();
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void ApplyEkycConcepts()
    {
        var userId = GetAuditUserId();
        foreach (var entry in base.ChangeTracker.Entries().ToList())
        {
            ApplyNigjeConcepts(entry, userId);
        }
    }
    
    private void ApplyNigjeConcepts(EntityEntry entry, long? userId)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                ApplyAbpConceptsForAddedEntity(entry, userId);
                break;
            case EntityState.Modified:
                ApplyAbpConceptsForModifiedEntity(entry, userId);
                break;
            case EntityState.Deleted:
                //Do nothing
                //It can be used for safe delete.
                break;
            case EntityState.Unchanged:
                //Do nothing.
                break;
        }
    }

    
    private void ApplyAbpConceptsForModifiedEntity(EntityEntry entry, long? userId)
    {
        if (entry.Entity is IAudit)
        {
            // Audit
        }
        if (entry.Entity is IModificationConcept modifiedEntity)
        {
            modifiedEntity.LastModificationTime = DateTime.Now;
            modifiedEntity.LastModifierUserId = userId;
        }
    }
    
    private void ApplyAbpConceptsForAddedEntity(EntityEntry entry, long? userId)
    {
        if (entry.Entity is ICreationConcept createdEntity)
        {
            createdEntity.CreationTime = DateTime.Now;
            createdEntity.CreatorUserId = userId;
        }
    }

    
    private long? GetAuditUserId()
    {
        //return the current user unique number.
        return RequestContext?.UserId;
    }

    
}
