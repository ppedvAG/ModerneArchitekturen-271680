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
using EasyBib.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure.Data;

public static class Seed
{
    /// <summary>Stichtag, auf den alle Leihfristen bezogen sind (fix, für deterministische Seeds).</summary>
    public static readonly DateOnly ReferenceDate = new(2026, 10, 8);

    /// <summary>
    /// Well-Known-Ids für Integrationstests. Öffentlich, damit Tests gegen
    /// konkrete Seed-Datensätze prüfen können, ohne Guids zu duplizieren.
    /// </summary>
    public static class Members
    {
        public static readonly Guid Fry = new("3f2a1b9c-6d4e-4a7c-9f10-1e2d3c4b5a69");
        public static readonly Guid Leela = new("8c1d4e7a-2b3f-4c8d-a9e0-5f6a7b8c9d01");
        public static readonly Guid Farnsworth = new("b7e8f9a0-1c2d-4e3f-b5a6-79804d1e2f30");
        public static readonly Guid Amy = new("d4c3b2a1-0f9e-4d8c-b7a6-5e4f3d2c1b0a");
        public static readonly Guid Bugs = new("e5d4c3b2-1a09-4f8e-c7d6-a5b4c3d2e1f0");
        public static readonly Guid Daffy = new("f6e5d4c3-2b1a-4a9d-d8c7-b6a5f4e3d2c1");
    }

    public static class Memberships
    {
        public static readonly Guid Fry = new("a1b2c3d4-5e6f-4a7b-8c9d-0e1f2a3b4c5d");
        public static readonly Guid Leela = new("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e");
        public static readonly Guid Farnsworth = new("c3d4e5f6-7a8b-4c9d-0e1f-2a3b4c5d6e7f");
        public static readonly Guid Amy = new("d4e5f6a7-8b9c-4d0e-1f2a-3b4c5d6e7f8a");
        public static readonly Guid Bugs = new("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b");
        public static readonly Guid Daffy = new("f6a7b8c9-0d1e-4f2a-3b4c-5d6e7f8a9b0c");
    }

    public static class MediaItems
    {
        public static readonly Guid BenderCookbook = new("10a9b8c7-6d5e-4f4a-b3c2-d1e0f9a8b7c6");
        public static readonly Guid FuturamaEncyclopedia = new("21b8c7d6-5e4f-4a5b-c2d1-e0f9a8b7c6d5");
        public static readonly Guid LooneyChronicle = new("32c7d6e5-4f5a-4b6c-d1e0-f9a8b7c6d5e4");
        public static readonly Guid RogerRabbit = new("43d6e5f4-5a6b-4c7d-e0f9-a8b7c6d5e4f3");
        public static readonly Guid SpaceJam = new("54e5f4a5-6b7c-4d8e-f9a8-b7c6d5e4f3a2");
        public static readonly Guid BackInAction = new("65f4a5b6-7c8d-4e9f-a8b7-c6d5e4f3a2b1");
        public static readonly Guid AcmeArsenal = new("76a5b6c7-8d9e-4f0a-b7c6-d5e4f3a2b1c0");
        public static readonly Guid FuturamaGame = new("87b6c7d8-9e0f-4a1b-c6d5-e4f3a2b1c0d9");
        public static readonly Guid MysteryOfLooney = new("98c7d8e9-0f1a-4b2c-d5e4-f3a2b1c0d9e8");
        public static readonly Guid BendersBigScore = new("a9d8e9f0-1a2b-4c3d-e4f3-a2b1c0d9e8f7");
    }

    public static class Loans
    {
        public static readonly Guid LeelaActive = new("1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c50");
        public static readonly Guid BugsActive = new("2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d61");
        public static readonly Guid AmyActive = new("3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e72");
        public static readonly Guid FryOverdue = new("4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f83");
        public static readonly Guid DaffyOverdue = new("5e6f7a8b-9c0d-4e1f-2a3b-4c5d6e7f8a94");
        public static readonly Guid FarnsworthReturned = new("6f7a8b9c-0d1e-4f2a-3b4c-5d6e7f8a9ba5");
        public static readonly Guid LeelaReturned = new("7a8b9c0d-1e2f-4a3b-4c5d-6e7f8a9bac06");
        public static readonly Guid BugsReturned = new("8b9c0d1e-2f3a-4b4c-5d6e-7f8a9bacbd17");
    }

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
                Id = Members.Fry,
                Name = "Philip J. Fry",
                Email = "philip.fry@planetexpress.de"
            },
            new Member
            {
                Id = Members.Leela,
                Name = "Turanga Leela",
                Email = "turanga.leela@planetexpress.de"
            },
            new Member
            {
                Id = Members.Farnsworth,
                Name = "Hubert J. Farnsworth",
                Email = "professor@farnsworth-labor.de"
            },
            new Member
            {
                Id = Members.Amy,
                Name = "Amy Wong",
                Email = "amy.wong@wong-industrien.de"
            },
            new Member
            {
                Id = Members.Bugs,
                Name = "Bugs Bunny",
                Email = "bugs.bunny@looney-tunes.de"
            },
            new Member
            {
                Id = Members.Daffy,
                Name = "Daffy Duck",
                Email = "daffy.duck@looney-tunes.de"
            }
        );
    }

    private static void SeedMemberships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Membership>().HasData(
            // Plan-Konfiguration gemäß Spezifikation: Basic=2/28, Premium=10/28, Family=5/28
            new Membership
            {
                Id = Memberships.Fry,
                MemberId = Members.Fry,
                PlanName = MembershipPlan.Basic,
                MaxActiveLoans = 2,
                LoanPeriodDays = 14
            },
            new Membership
            {
                Id = Memberships.Leela,
                MemberId = Members.Leela,
                PlanName = MembershipPlan.Premium,
                MaxActiveLoans = 5,
                LoanPeriodDays = 28
            },
            new Membership
            {
                Id = Memberships.Farnsworth,
                MemberId = Members.Farnsworth,
                PlanName = MembershipPlan.Basic,
                MaxActiveLoans = 2,
                LoanPeriodDays = 14
            },
            new Membership
            {
                Id = Memberships.Amy,
                MemberId = Members.Amy,
                PlanName = MembershipPlan.Family,
                MaxActiveLoans = 8,
                LoanPeriodDays = 21
            },
            new Membership
            {
                Id = Memberships.Bugs,
                MemberId = Members.Bugs,
                PlanName = MembershipPlan.Family,
                MaxActiveLoans = 8,
                LoanPeriodDays = 21
            },
            new Membership
            {
                Id = Memberships.Daffy,
                MemberId = Members.Daffy,
                PlanName = MembershipPlan.Premium,
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
                Id = MediaItems.BenderCookbook,
                Title = "Benders großes Kochbuch: 100 Rezepte mit Bier",
                EAN = "9783866812345",
                Type = MediaType.Book
            },
            new MediaItem
            {
                Id = MediaItems.FuturamaEncyclopedia,
                Title = "Futurama: Die offizielle Enzyklopädie des 31. Jahrhunderts",
                EAN = "9783442378901",
                Type = MediaType.Book
            },
            new MediaItem
            {
                Id = MediaItems.LooneyChronicle,
                Title = "Was oper, Doc? – Die große Looney-Tunes-Chronik",
                EAN = "9783551312456",
                Type = MediaType.Book
            },
            new MediaItem
            {
                Id = MediaItems.RogerRabbit,
                Title = "Falsches Spiel mit Roger Rabbit",
                EAN = "5051892345678",
                Type = MediaType.Movie
            },
            new MediaItem
            {
                Id = MediaItems.SpaceJam,
                Title = "Space Jam",
                EAN = "8839295674012",
                Type = MediaType.Movie
            },
            new MediaItem
            {
                Id = MediaItems.BackInAction,
                Title = "Looney Tunes: Back in Action",
                EAN = "7321950286734",
                Type = MediaType.Movie
            },
            new MediaItem
            {
                Id = MediaItems.AcmeArsenal,
                Title = "Looney Tunes: Acme Arsenal",
                EAN = "5030930124567",
                Type = MediaType.Game
            },
            new MediaItem
            {
                Id = MediaItems.FuturamaGame,
                Title = "Futurama: Das Videospiel",
                EAN = "5035229098765",
                Type = MediaType.Game
            },
            new MediaItem
            {
                Id = MediaItems.MysteryOfLooney,
                Title = "Looney Tunes: World of Mayhem",
                EAN = "4035229112233",
                Type = MediaType.Game
            },
            new MediaItem
            {
                Id = MediaItems.BendersBigScore,
                Title = "Futurama: Benders großer Coup",
                EAN = "8839295674321",
                Type = MediaType.Movie
            }
        );
    }

    private static void SeedLoans(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>().HasData(
            // Aktive Ausleihen: DueDate in der Zukunft relativ zum Stichtag
            new Loan
            {
                Id = Loans.LeelaActive,
                MembershipId = Memberships.Leela,
                MediaItemId = MediaItems.FuturamaEncyclopedia,
                Status = LoanStatus.Active,
                DueDate = ReferenceDate.AddDays(12)
            },
            new Loan
            {
                Id = Loans.BugsActive,
                MembershipId = Memberships.Bugs,
                MediaItemId = MediaItems.AcmeArsenal,
                Status = LoanStatus.Active,
                DueDate = ReferenceDate.AddDays(19)
            },
            new Loan
            {
                Id = Loans.AmyActive,
                MembershipId = Memberships.Amy,
                MediaItemId = MediaItems.BendersBigScore,
                Status = LoanStatus.Active,
                DueDate = ReferenceDate.AddDays(21)
            },
            // Überfällig: DueDate in der Vergangenheit
            new Loan
            {
                Id = Loans.FryOverdue,
                MembershipId = Memberships.Fry,
                MediaItemId = MediaItems.BenderCookbook,
                Status = LoanStatus.Overdue,
                DueDate = ReferenceDate.AddDays(-9)
            },
            new Loan
            {
                Id = Loans.DaffyOverdue,
                MembershipId = Memberships.Daffy,
                MediaItemId = MediaItems.SpaceJam,
                Status = LoanStatus.Overdue,
                DueDate = ReferenceDate.AddDays(-4)
            },
            // Zurückgegeben: DueDate in der Vergangenheit
            new Loan
            {
                Id = Loans.FarnsworthReturned,
                MembershipId = Memberships.Farnsworth,
                MediaItemId = MediaItems.RogerRabbit,
                Status = LoanStatus.Returned,
                DueDate = ReferenceDate.AddDays(-30)
            },
            new Loan
            {
                Id = Loans.LeelaReturned,
                MembershipId = Memberships.Leela,
                MediaItemId = MediaItems.FuturamaGame,
                Status = LoanStatus.Returned,
                DueDate = ReferenceDate.AddDays(-16)
            },
            new Loan
            {
                Id = Loans.BugsReturned,
                MembershipId = Memberships.Bugs,
                MediaItemId = MediaItems.LooneyChronicle,
                Status = LoanStatus.Returned,
                DueDate = ReferenceDate.AddDays(-2)
            }
        );
    }
}