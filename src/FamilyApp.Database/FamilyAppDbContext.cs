using Microsoft.EntityFrameworkCore;
using FamilyApp.Models.Entities;

namespace FamilyApp.Database;

public class FamilyAppDbContext : DbContext
{
    public FamilyAppDbContext(DbContextOptions<FamilyAppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<TherapySession> TherapySessions { get; set; }
    public DbSet<CareTeamMember> CareTeamMembers { get; set; }
    public DbSet<SessionProgress> SessionProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Children)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TherapySession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasOne(e => e.Child)
                  .WithMany(c => c.TherapySessions)
                  .HasForeignKey(e => e.ChildId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CareTeamMember>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<SessionProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProgressNotes).HasMaxLength(1000);
            entity.Property(e => e.CompletionPercentage).HasPrecision(5, 2);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.HasOne(e => e.Session)
                  .WithMany(s => s.SessionProgresses)
                  .HasForeignKey(e => e.SessionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
