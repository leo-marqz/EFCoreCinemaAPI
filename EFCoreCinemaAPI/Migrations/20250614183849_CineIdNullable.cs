using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CineIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CineOffers_Cines_CineId",
                table: "CineOffers");

            migrationBuilder.DropIndex(
                name: "IX_CineOffers_CineId",
                table: "CineOffers");

            migrationBuilder.AlterColumn<int>(
                name: "CineId",
                table: "CineOffers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_CineOffers_CineId",
                table: "CineOffers",
                column: "CineId",
                unique: true,
                filter: "[CineId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CineOffers_Cines_CineId",
                table: "CineOffers",
                column: "CineId",
                principalTable: "Cines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CineOffers_Cines_CineId",
                table: "CineOffers");

            migrationBuilder.DropIndex(
                name: "IX_CineOffers_CineId",
                table: "CineOffers");

            migrationBuilder.AlterColumn<int>(
                name: "CineId",
                table: "CineOffers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 11, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_CineOffers_CineId",
                table: "CineOffers",
                column: "CineId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CineOffers_Cines_CineId",
                table: "CineOffers",
                column: "CineId",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
