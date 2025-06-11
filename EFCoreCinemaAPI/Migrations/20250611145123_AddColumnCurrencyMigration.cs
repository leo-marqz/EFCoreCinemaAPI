using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCurrencyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "CineRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 11, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CineRooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Currency",
                value: "$");

            migrationBuilder.UpdateData(
                table: "CineRooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Currency",
                value: "$");

            migrationBuilder.UpdateData(
                table: "CineRooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Currency",
                value: "$");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "CineRooms");

            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
