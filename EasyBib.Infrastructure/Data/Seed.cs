// Seed.cs — Infrastructure-Projekt (EasyBib.Infrastructure)
//
// Einbindung im EasyBibDbContext:
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//         modelBuilder.ApplySeed();
//     }
//
// Hinweise:
// - Alle Guids sind konstante v4-Guids, damit HasData deterministisch ist
//   und die generierte Migration stabil bleibt (kein DateTime.Now, kein Random).
// - Alle Datumsangaben sind fixe DateOnly-Werte relativ zum konstanten
//   Referenzstichtag (08.10.2026):
//     Active  -> DueDate in der Zukunft
//     Overdue -> DueDate in der Vergangenheit
//     Returned-> DueDate in der Vergangenheit

using EasyBib.Domain;
using EasyBib.Domain.Enums;
using EasyBib.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure.Data;

public static class Seed
{
    /// <summary>Stichtag, auf den alle Leihfristen bezogen sind.</summary>
    private static readonly DateOnly ReferenceDate = new(2026, 10, 8);

    // ---------- Member-Ids ----------
    private static readonly Guid MemberFryId = new("3f2a1b9c-6d4e-4a7c-9f10-1e2d3c4b5a69");
    private static readonly Guid MemberLeelaId = new("8c1d4e7a-2b3f-4c8d-a9e0-5f6a7b8c9d01");
    private static readonly Guid MemberFarnsworthId = new("b7e8f9a0-1c2d-4e3f-b5a6-79804d1e2f30");
    private static readonly Guid MemberAmyId = new("d4c3b2a1-0f9e-4d8c-b7a6-5e4f3d2c1b0a");
    private static readonly Guid MemberBugsId = new("e5d4c3b2-1a09-4f8e-c7d6-a5b4c3d2e1f0");
    private static readonly Guid MemberDaffyId = new("f6e5d4c3-2b1a-4a9d-d8c7-b6a5f4e3d2c1");

    // ---------- Membership-Ids ----------
    private static readonly Guid MembershipFryId = new("a1b2c3d4-5e6f-4a7b-8c9d-0e1f2a3b4c5d");
    private static readonly Guid MembershipLeelaId = new("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e");
    private static readonly Guid MembershipFarnsworthId = new("c3d4e5f6-7a8b-4c9d-0e1f-2a3b4c5d6e7f");
    private static readonly Guid MembershipAmyId = new("d4e5f6a7-8b9c-4d0e-1f2a-3b4c5d6e7f8a");
    private static readonly Guid MembershipBugsId = new("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b");
    private static readonly Guid MembershipDaffyId = new("f6a7b8c9-0d1e-4f2a-3b4c-5d6e7f8a9b0c");

    // ---------- MediaItem-Ids ----------
    private static readonly Guid MediaBenderCookbookId = new("10a9b8c7-6d5e-4f4a-b3c2-d1e0f9a8b7c6");
    private static readonly Guid MediaFuturamaEncyId = new("21b8c7d6-5e4f-4a5b-c2d1-e0f9a8b7c6d5");
    private static readonly Guid MediaLooneyChronicleId = new("32c7d6e5-4f5a-4b6c-d1e0-f9a8b7c6d5e4");
    private static readonly Guid MediaRogerRabbitId = new("43d6e5f4-5a6b-4c7d-e0f9-a8b7c6d5e4f3");
    private static readonly Guid MediaSpaceJamId = new("54e5f4a5-6b7c-4d8e-f9a8-b7c6d5e4f3a2");
    private static readonly Guid MediaBackInActionId = new("65f4a5b6-7c8d-4e9f-a8b7-c6d5e4f3a2b1");
    private static readonly Guid MediaAcmeArsenalId = new("76a5b6c7-8d9e-4f0a-b7c6-d5e4f3a2b1c0");
    private static readonly Guid MediaFuturamaGameId = new("87b6c7d8-9e0f-4a1b-c6d5-e4f3a2b1c0d9");
    private static readonly Guid MediaMysteryOfLooneyId = new("98c7d8e9-0f1a-4b2c-d5e4-f3a2b1c0d9e8");
    private static readonly Guid MediaBendersBigScoreId = new("a9d8e9f0-1a2b-4c3d-e4f3-a2b1c0d9e8f7");

    // ---------- Loan-Ids ----------
    private static readonly Guid Loan1Id = new("1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c50");
    private static readonly Guid Loan2Id = new("2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d61");
    private static readonly Guid Loan3Id = new("3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e72");
    private static readonly Guid Loan4Id = new("4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f83");
    private static readonly Guid Loan5Id = new("5e6f7a8b-9c0d-4e1f-2a3b-4c5d6e7f8a94");
    private static readonly Guid Loan6Id = new("6f7a8b9c-0d1e-4f2a-3b4c-5d6e7f8a9ba5");
    private static readonly Guid Loan7Id = new("7a8b9c0d-1e2f-4a3b-4c5d-6e7f8a9bac06");
    private static readonly Guid Loan8Id = new("8b9c0d1e-2f3a-4b4c-5d6e-7f8a9bacbd17");

    public static void ApplySeed(this ModelBuilder modelBuilder)
    {
        SeedMembers(modelBuilder);
        SeedMemberships(modelBuilder);
        SeedMediaItems(modelBuilder);
        SeedLoans(modelBuilder);
    }

    private static void SeedMembers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasData(
            new Member
            {
                Id = MemberFryId,
                Name = "Philip J. Fry",
                Email = "philip.fry@planetexpress.de"
            },
            new Member
            {
                Id = MemberLeelaId,
                Name = "Turanga Leela",
                Email = "turanga.leela@planetexpress.de"
            },
            new Member
            {
                Id = MemberFarnsworthId,
                Name = "Hubert J. Farnsworth",
                Email = "professor@farnsworth-labor.de"
            },
            new Member
            {
                Id = MemberAmyId,
                Name = "Amy Wong",
                Email = "amy.wong@wong-industrien.de"
            },
            new Member
            {
                Id = MemberBugsId,
                Name = "Bugs Bunny",
                Email = "bugs.bunny@looney-tunes.de"
            },
            new Member
            {
                Id = MemberDaffyId,
                Name = "Daffy Duck",
                Email = "daffy.duck@looney-tunes.de"
            }
        );
    }

    private static void SeedMemberships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Membership>().HasData(
            new Membership
            {
                Id = MembershipFryId,
                MemberId = MemberFryId,
                PlanName = PlanName.Basic,
                MaxActiveLoans = 2,
                LoanPeriodDays = 14
            },
            new Membership
            {
                Id = MembershipLeelaId,
                MemberId = MemberLeelaId,
                PlanName = PlanName.Premium,
                MaxActiveLoans = 5,
                LoanPeriodDays = 28
            },
            new Membership
            {
                Id = MembershipFarnsworthId,
                MemberId = MemberFarnsworthId,
                PlanName = PlanName.Basic,
                MaxActiveLoans = 2,
                LoanPeriodDays = 14
            },
            new Membership
            {
                Id = MembershipAmyId,
                MemberId = MemberAmyId,
                PlanName = PlanName.Family,
                MaxActiveLoans = 8,
                LoanPeriodDays = 21
            },
            new Membership
            {
                Id = MembershipBugsId,
                MemberId = MemberBugsId,
                PlanName = PlanName.Family,
                MaxActiveLoans = 8,
                LoanPeriodDays = 21
            },
            new Membership
            {
                Id = MembershipDaffyId,
                MemberId = MemberDaffyId,
                PlanName = PlanName.Premium,
                MaxActiveLoans = 5,
                LoanPeriodDays = 28
            }
        );
    }

    private static void SeedMediaItems(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaItem>().HasData(
            new MediaItem
            {
                Id = MediaBenderCookbookId,
                Title = "Benders großes Kochbuch: 100 Rezepte mit Bier",
                EAN = "9783866812345",
                Type = MediaType.Book
            },
            new MediaItem
            {
                Id = MediaFuturamaEncyId,
                Title = "Futurama: Die offizielle Enzyklopädie des 31. Jahrhunderts",
                EAN = "9783442378901",
                Type = MediaType.Book
            },
            new MediaItem
            {
                Id = MediaLooneyChronicleId,
                Title = "Was oper, Doc? – Die große Looney-Tunes-Chronik",
                EAN = "9783551312456",
                Type = MediaType.Book
            },
            new MediaItem
            {
                Id = MediaRogerRabbitId,
                Title = "Falsches Spiel mit Roger Rabbit",
                EAN = "5051892345678",
                Type = MediaType.Movie
            },
            new MediaItem
            {
                Id = MediaSpaceJamId,
                Title = "Space Jam",
                EAN = "8839295674012",
                Type = MediaType.Movie
            },
            new MediaItem
            {
                Id = MediaBackInActionId,
                Title = "Looney Tunes: Back in Action",
                EAN = "7321950286734",
                Type = MediaType.Movie
            },
            new MediaItem
            {
                Id = MediaAcmeArsenalId,
                Title = "Looney Tunes: Acme Arsenal",
                EAN = "5030930124567",
                Type = MediaType.Game
            },
            new MediaItem
            {
                Id = MediaFuturamaGameId,
                Title = "Futurama: Das Videospiel",
                EAN = "5035229098765",
                Type = MediaType.Game
            },
            new MediaItem
            {
                Id = MediaMysteryOfLooneyId,
                Title = "Looney Tunes: World of Mayhem",
                EAN = "4035229112233",
                Type = MediaType.Game
            },
            new MediaItem
            {
                Id = MediaBendersBigScoreId,
                Title = "Futurama: Benders großer Coup",
                EAN = "8839295674321",
                Type = MediaType.Movie
            }
        );
    }

    private static void SeedLoans(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>().HasData(
            // Aktive Ausleihen: DueDate in der Zukunft (relativ zum Stichtag)
            new Loan
            {
                Id = Loan1Id,
                MembershipId = MembershipLeelaId,
                MediaItemId = MediaFuturamaEncyId,
                Status = LoanStatus.Active,
                DueDate = ReferenceDate.AddDays(12)   // 20.10.2026
            },
            new Loan
            {
                Id = Loan2Id,
                MembershipId = MembershipBugsId,
                MediaItemId = MediaAcmeArsenalId,
                Status = LoanStatus.Active,
                DueDate = ReferenceDate.AddDays(19)   // 27.10.2026
            },
            new Loan
            {
                Id = Loan3Id,
                MembershipId = MembershipAmyId,
                MediaItemId = MediaBendersBigScoreId,
                Status = LoanStatus.Active,
                DueDate = ReferenceDate.AddDays(21)   // 29.10.2026
            },
            // Überfällig: DueDate in der Vergangenheit
            new Loan
            {
                Id = Loan4Id,
                MembershipId = MembershipFryId,
                MediaItemId = MediaBenderCookbookId,
                Status = LoanStatus.Overdue,
                DueDate = ReferenceDate.AddDays(-9)   // 29.09.2026
            },
            new Loan
            {
                Id = Loan5Id,
                MembershipId = MembershipDaffyId,
                MediaItemId = MediaSpaceJamId,
                Status = LoanStatus.Overdue,
                DueDate = ReferenceDate.AddDays(-4)   // 04.10.2026
            },
            // Zurückgegeben: DueDate in der Vergangenheit
            new Loan
            {
                Id = Loan6Id,
                MembershipId = MembershipFarnsworthId,
                MediaItemId = MediaRogerRabbitId,
                Status = LoanStatus.Returned,
                DueDate = ReferenceDate.AddDays(-30)  // 08.09.2026
            },
            new Loan
            {
                Id = Loan7Id,
                MembershipId = MembershipLeelaId,
                MediaItemId = MediaFuturamaGameId,
                Status = LoanStatus.Returned,
                DueDate = ReferenceDate.AddDays(-16)  // 22.09.2026
            },
            new Loan
            {
                Id = Loan8Id,
                MembershipId = MembershipBugsId,
                MediaItemId = MediaLooneyChronicleId,
                Status = LoanStatus.Returned,
                DueDate = ReferenceDate.AddDays(-2)   // 06.10.2026
            }
        );
    }
}