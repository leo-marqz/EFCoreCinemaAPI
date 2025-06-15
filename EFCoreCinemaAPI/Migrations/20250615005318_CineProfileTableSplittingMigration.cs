using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CineProfileTableSplittingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeOfEthics",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoreValues",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mision",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vision",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CineId1",
                table: "CineOffers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CineId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CineOffers_CineId1",
                table: "CineOffers",
                column: "CineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CineOffers_Cines_CineId1",
                table: "CineOffers",
                column: "CineId1",
                principalTable: "Cines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CineOffers_Cines_CineId1",
                table: "CineOffers");

            migrationBuilder.DropIndex(
                name: "IX_CineOffers_CineId1",
                table: "CineOffers");

            migrationBuilder.DropColumn(
                name: "CodeOfEthics",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "CoreValues",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Mision",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Vision",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "CineId1",
                table: "CineOffers");
        }
    }
}
