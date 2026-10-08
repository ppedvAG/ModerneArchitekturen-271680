using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyBib.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitEasyBibData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanName = table.Column<int>(type: "int", nullable: false),
                    MaxActiveLoans = table.Column<int>(type: "int", nullable: false),
                    LoanPeriodDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_MediaItems_MediaItemId",
                        column: x => x.MediaItemId,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "EAN", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("10a9b8c7-6d5e-4f4a-b3c2-d1e0f9a8b7c6"), "9783866812345", "Benders großes Kochbuch: 100 Rezepte mit Bier", 0 },
                    { new Guid("21b8c7d6-5e4f-4a5b-c2d1-e0f9a8b7c6d5"), "9783442378901", "Futurama: Die offizielle Enzyklopädie des 31. Jahrhunderts", 0 },
                    { new Guid("32c7d6e5-4f5a-4b6c-d1e0-f9a8b7c6d5e4"), "9783551312456", "Was oper, Doc? – Die große Looney-Tunes-Chronik", 0 },
                    { new Guid("43d6e5f4-5a6b-4c7d-e0f9-a8b7c6d5e4f3"), "5051892345678", "Falsches Spiel mit Roger Rabbit", 2 },
                    { new Guid("54e5f4a5-6b7c-4d8e-f9a8-b7c6d5e4f3a2"), "8839295674012", "Space Jam", 2 },
                    { new Guid("65f4a5b6-7c8d-4e9f-a8b7-c6d5e4f3a2b1"), "7321950286734", "Looney Tunes: Back in Action", 2 },
                    { new Guid("76a5b6c7-8d9e-4f0a-b7c6-d5e4f3a2b1c0"), "5030930124567", "Looney Tunes: Acme Arsenal", 1 },
                    { new Guid("87b6c7d8-9e0f-4a1b-c6d5-e4f3a2b1c0d9"), "5035229098765", "Futurama: Das Videospiel", 1 },
                    { new Guid("98c7d8e9-0f1a-4b2c-d5e4-f3a2b1c0d9e8"), "4035229112233", "Looney Tunes: World of Mayhem", 1 },
                    { new Guid("a9d8e9f0-1a2b-4c3d-e4f3-a2b1c0d9e8f7"), "8839295674321", "Futurama: Benders großer Coup", 2 }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("3f2a1b9c-6d4e-4a7c-9f10-1e2d3c4b5a69"), "philip.fry@planetexpress.de", "Philip J. Fry" },
                    { new Guid("8c1d4e7a-2b3f-4c8d-a9e0-5f6a7b8c9d01"), "turanga.leela@planetexpress.de", "Turanga Leela" },
                    { new Guid("b7e8f9a0-1c2d-4e3f-b5a6-79804d1e2f30"), "professor@farnsworth-labor.de", "Hubert J. Farnsworth" },
                    { new Guid("d4c3b2a1-0f9e-4d8c-b7a6-5e4f3d2c1b0a"), "amy.wong@wong-industrien.de", "Amy Wong" },
                    { new Guid("e5d4c3b2-1a09-4f8e-c7d6-a5b4c3d2e1f0"), "bugs.bunny@looney-tunes.de", "Bugs Bunny" },
                    { new Guid("f6e5d4c3-2b1a-4a9d-d8c7-b6a5f4e3d2c1"), "daffy.duck@looney-tunes.de", "Daffy Duck" }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "LoanPeriodDays", "MaxActiveLoans", "MemberId", "PlanName" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-5e6f-4a7b-8c9d-0e1f2a3b4c5d"), 14, 2, new Guid("3f2a1b9c-6d4e-4a7c-9f10-1e2d3c4b5a69"), 0 },
                    { new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e"), 28, 5, new Guid("8c1d4e7a-2b3f-4c8d-a9e0-5f6a7b8c9d01"), 3 },
                    { new Guid("c3d4e5f6-7a8b-4c9d-0e1f-2a3b4c5d6e7f"), 14, 2, new Guid("b7e8f9a0-1c2d-4e3f-b5a6-79804d1e2f30"), 0 },
                    { new Guid("d4e5f6a7-8b9c-4d0e-1f2a-3b4c5d6e7f8a"), 21, 8, new Guid("d4c3b2a1-0f9e-4d8c-b7a6-5e4f3d2c1b0a"), 2 },
                    { new Guid("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b"), 21, 8, new Guid("e5d4c3b2-1a09-4f8e-c7d6-a5b4c3d2e1f0"), 2 },
                    { new Guid("f6a7b8c9-0d1e-4f2a-3b4c-5d6e7f8a9b0c"), 28, 5, new Guid("f6e5d4c3-2b1a-4a9d-d8c7-b6a5f4e3d2c1"), 3 }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "DueDate", "MediaItemId", "MembershipId", "Status" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c50"), new DateOnly(2026, 10, 20), new Guid("21b8c7d6-5e4f-4a5b-c2d1-e0f9a8b7c6d5"), new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e"), 0 },
                    { new Guid("2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d61"), new DateOnly(2026, 10, 27), new Guid("76a5b6c7-8d9e-4f0a-b7c6-d5e4f3a2b1c0"), new Guid("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b"), 0 },
                    { new Guid("3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e72"), new DateOnly(2026, 10, 29), new Guid("a9d8e9f0-1a2b-4c3d-e4f3-a2b1c0d9e8f7"), new Guid("d4e5f6a7-8b9c-4d0e-1f2a-3b4c5d6e7f8a"), 0 },
                    { new Guid("4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f83"), new DateOnly(2026, 9, 29), new Guid("10a9b8c7-6d5e-4f4a-b3c2-d1e0f9a8b7c6"), new Guid("a1b2c3d4-5e6f-4a7b-8c9d-0e1f2a3b4c5d"), 2 },
                    { new Guid("5e6f7a8b-9c0d-4e1f-2a3b-4c5d6e7f8a94"), new DateOnly(2026, 10, 4), new Guid("54e5f4a5-6b7c-4d8e-f9a8-b7c6d5e4f3a2"), new Guid("f6a7b8c9-0d1e-4f2a-3b4c-5d6e7f8a9b0c"), 2 },
                    { new Guid("6f7a8b9c-0d1e-4f2a-3b4c-5d6e7f8a9ba5"), new DateOnly(2026, 9, 8), new Guid("43d6e5f4-5a6b-4c7d-e0f9-a8b7c6d5e4f3"), new Guid("c3d4e5f6-7a8b-4c9d-0e1f-2a3b4c5d6e7f"), 1 },
                    { new Guid("7a8b9c0d-1e2f-4a3b-4c5d-6e7f8a9bac06"), new DateOnly(2026, 9, 22), new Guid("87b6c7d8-9e0f-4a1b-c6d5-e4f3a2b1c0d9"), new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e"), 1 },
                    { new Guid("8b9c0d1e-2f3a-4b4c-5d6e-7f8a9bacbd17"), new DateOnly(2026, 10, 6), new Guid("32c7d6e5-4f5a-4b6c-d1e0-f9a8b7c6d5e4"), new Guid("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_MediaItemId",
                table: "Loans",
                column: "MediaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_MembershipId",
                table: "Loans",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_EAN",
                table: "MediaItems",
                column: "EAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_MemberId",
                table: "Memberships",
                column: "MemberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "MediaItems");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
