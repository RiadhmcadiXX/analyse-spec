using System.Reflection;
using System.Reflection.Emit;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Entities.Books;
using Domain.Entities.DataLogs;
using Domain.Entities.Identity;
using Domain.Entities.SPEC;
using Domain.Entities.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;
using Persistence.Interceptors;

namespace Persistence;

public class AnalyseSpecDbContext : IdentityDbContext<User, Role, Guid,
    IdentityUserClaim<Guid>, UserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly AuditableAndSoftDeletableEntitySaveChangesInterceptor
        _auditableAndSoftDeletableEntitySaveChangesInterceptor = null!;

    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor = null!;
    private readonly UserSaveChangesInterceptor _userSaveChangesInterceptor = null!;
    private readonly EntitySaveChangesInterceptor _entitySaveChangesInterceptor = null!;

    public AnalyseSpecDbContext(
        DbContextOptions<AnalyseSpecDbContext> options,
        AuditableAndSoftDeletableEntitySaveChangesInterceptor auditableAndSoftDeletableEntitySaveChangesInterceptor,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        UserSaveChangesInterceptor userSaveChangesInterceptor,
        EntitySaveChangesInterceptor entitySaveChangesInterceptor)
        : base(options)
    {
        _auditableAndSoftDeletableEntitySaveChangesInterceptor = auditableAndSoftDeletableEntitySaveChangesInterceptor;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _userSaveChangesInterceptor = userSaveChangesInterceptor;
        _entitySaveChangesInterceptor = entitySaveChangesInterceptor;
    }

    public DbSet<Administrator> Administrators { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    //DbSet relative to SPEC
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Option> Options { get; set; } = null!;
    
    public DbSet<Reject> Rejects { get; set; } = null!;
    
    public DbSet<Programme> Programmes { get; set; } = null!;   
    
    

    public AnalyseSpecDbContext()
    {
    }

    public AnalyseSpecDbContext(DbContextOptions<AnalyseSpecDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        builder.Entity<User>()
            .HasMany(u => u.Programmes)
            .WithMany(p => p.UserProgrammes)
            .UsingEntity(j => j.ToTable("UserProgrammes"));
        
        
        base.OnModelCreating(builder);

        // Global query to prevent loading soft-deleted entities
        
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                continue;

            if (entityType.ClrType == typeof(User))
                continue;

            entityType.AddSoftDeleteQueryFilter();
        }
        
        
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(
            _auditableAndSoftDeletableEntitySaveChangesInterceptor,
            _auditableEntitySaveChangesInterceptor,
            _userSaveChangesInterceptor,
            _entitySaveChangesInterceptor);
    }

    public async Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        return await base.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }
}