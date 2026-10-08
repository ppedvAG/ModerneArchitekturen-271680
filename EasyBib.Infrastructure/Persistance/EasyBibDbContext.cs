using EasyBib.Domain;
using EasyBib.Domain.Models;
using EasyBib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure.Persistance;

public class EasyBibDbContext : DbContext
{
    public EasyBibDbContext(DbContextOptions<EasyBibDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<MediaItem> MediaItems => Set<MediaItem>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Member <-> Membership (1 : 0..1)
        modelBuilder.Entity<Member>()
            .HasOne(m => m.Membership)
            .WithOne(m => m.Member)
            .HasForeignKey<Membership>(m => m.MemberId);

        // Loan -> Membership (n : 1)
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Membership)
            .WithMany()
            .HasForeignKey(l => l.MembershipId);

        // Loan -> MediaItem (n : 1)
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.MediaItem)
            .WithMany()
            .HasForeignKey(l => l.MediaItemId);

        // MediaItem.EAN als eindeutiger Index
        modelBuilder.Entity<MediaItem>()
            .HasIndex(m => m.EAN)
            .IsUnique();

        modelBuilder.ApplySeed();
    }
}