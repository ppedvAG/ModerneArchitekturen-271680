using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure.Data;

public static class SeedIdentity
{
    public static class Roles
    {
        public static readonly Guid Admin =
            new("11111111-1111-4111-8111-111111111111");

        public static readonly Guid Librarian =
            new("22222222-2222-4222-8222-222222222222");

        public static readonly Guid Member =
            new("33333333-3333-4333-8333-333333333333");
    }

    public static class Users
    {
        public static readonly Guid Fry =
            new("44444444-4444-4444-8444-444444444444");

        public static readonly Guid Leela =
            new("55555555-5555-4555-8555-555555555555");

        public static readonly Guid Farnsworth =
            new("66666666-6666-4666-8666-666666666666");
    }

    public static void ApplySeedIdentity(this ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        SeedUsers(modelBuilder);
        SeedUserRoles(modelBuilder);
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Id = Roles.Admin,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Roles.Admin.ToString()
            },
            new IdentityRole<Guid>
            {
                Id = Roles.Librarian,
                Name = "Librarian",
                NormalizedName = "LIBRARIAN",
                ConcurrencyStamp = Roles.Librarian.ToString()
            },
            new IdentityRole<Guid>
            {
                Id = Roles.Member,
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = Roles.Member.ToString()
            }
        );
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser<Guid>>().HasData(
            new IdentityUser<Guid>
            {
                Id = Users.Fry,
                UserName = "fry",
                NormalizedUserName = "FRY",
                Email = "philip.fry@planetexpress.de",
                NormalizedEmail = "PHILIP.FRY@PLANETEXPRESS.DE",
                EmailConfirmed = true,
                PasswordHash =
                    "AQAAAAIAAYagAAAAEK9Z7Q9Y9L5J4J5J6R4Y5R7Q9R3K2X8L1J7N8M9P0Q1R2S3T4U5V6W7X8Y9Z0",
                SecurityStamp = Users.Fry.ToString(),
                ConcurrencyStamp = Users.Fry.ToString()
            },
            new IdentityUser<Guid>
            {
                Id = Users.Leela,
                UserName = "leela",
                NormalizedUserName = "LEELA",
                Email = "turanga.leela@planetexpress.de",
                NormalizedEmail = "TURANGA.LEELA@PLANETEXPRESS.DE",
                EmailConfirmed = true,
                PasswordHash =
                    "AQAAAAIAAYagAAAAEK9Z7Q9Y9L5J4J5J6R4Y5R7Q9R3K2X8L1J7N8M9P0Q1R2S3T4U5V6W7X8Y9Z0",
                SecurityStamp = Users.Leela.ToString(),
                ConcurrencyStamp = Users.Leela.ToString()
            },
            new IdentityUser<Guid>
            {
                Id = Users.Farnsworth,
                UserName = "farnsworth",
                NormalizedUserName = "FARNSWORTH",
                Email = "professor@farnsworth-labor.de",
                NormalizedEmail = "PROFESSOR@FARNSWORTH-LABOR.DE",
                EmailConfirmed = true,
                PasswordHash =
                    "AQAAAAIAAYagAAAAEK9Z7Q9Y9L5J4J5J6R4Y5R7Q9R3K2X8L1J7N8M9P0Q1R2S3T4U5V6W7X8Y9Z0",
                SecurityStamp = Users.Farnsworth.ToString(),
                ConcurrencyStamp = Users.Farnsworth.ToString()
            }
        );
    }

    private static void SeedUserRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            // Fry -> Admin
            new IdentityUserRole<Guid>
            {
                UserId = Users.Fry,
                RoleId = Roles.Admin
            },

            // Leela -> Librarian
            new IdentityUserRole<Guid>
            {
                UserId = Users.Leela,
                RoleId = Roles.Librarian
            },

            // Farnsworth -> Member
            new IdentityUserRole<Guid>
            {
                UserId = Users.Farnsworth,
                RoleId = Roles.Member
            }
        );
    }
}