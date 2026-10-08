using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyBib.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e"),
                column: "PlanName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-8b9c-4d0e-1f2a-3b4c5d6e7f8a"),
                column: "PlanName",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b"),
                column: "PlanName",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-0d1e-4f2a-3b4c-5d6e7f8a9b0c"),
                column: "PlanName",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e"),
                column: "PlanName",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-8b9c-4d0e-1f2a-3b4c5d6e7f8a"),
                column: "PlanName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-9c0d-4e1f-2a3b-4c5d6e7f8a9b"),
                column: "PlanName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-0d1e-4f2a-3b4c-5d6e7f8a9b0c"),
                column: "PlanName",
                value: 3);
        }
    }
}
