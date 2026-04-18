using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskItem>()
            .HasQueryFilter(t => !t.IsDeleted);

        modelBuilder.Entity<TaskItem>()
           .HasOne(t => t.AssignedUser)
           .WithMany()
           .HasForeignKey(t => t.AssignedUserId)
           .OnDelete(DeleteBehavior.SetNull)
           .IsRequired(false);

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<string>()
            .HasDefaultValue(TaskItemStatus.Pending);

        modelBuilder.Entity<User>()
            .HasQueryFilter(t => !t.IsDeleted);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

    }
}