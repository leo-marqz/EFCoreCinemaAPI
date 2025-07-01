using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE Genre_GetGenreById
                @Id INT
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT * FROM Genres WHERE Id = @Id;
            END
        ");

                    migrationBuilder.Sql(@"
            CREATE PROCEDURE Genre_InsertNewGenre
                @Name NVARCHAR(150)
                , @Id INT OUTPUT
            AS
            BEGIN
                SET NOCOUNT ON;
                INSERT INTO Genres (Name) VALUES (@Name);
                SELECT @Id = SCOPE_IDENTITY();
            END
        ");

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 16, 22, 25, 31, 122, DateTimeKind.Local).AddTicks(2348));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 17, 12, 25, 31, 122, DateTimeKind.Local).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 16, 22, 25, 31, 122, DateTimeKind.Local).AddTicks(4632));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 17, 17, 25, 31, 122, DateTimeKind.Local).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 22, 25, 31, 117, DateTimeKind.Local).AddTicks(4206));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 17, 19, 25, 31, 117, DateTimeKind.Local).AddTicks(4222));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS Genre_GetGenreById;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS Genre_InsertNewGenre;");

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 16, 22, 23, 11, 265, DateTimeKind.Local).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 17, 12, 23, 11, 265, DateTimeKind.Local).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 16, 22, 23, 11, 266, DateTimeKind.Local).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 17, 17, 23, 11, 266, DateTimeKind.Local).AddTicks(868));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 22, 23, 11, 260, DateTimeKind.Local).AddTicks(3340));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 17, 19, 23, 11, 260, DateTimeKind.Local).AddTicks(3354));
        }
    }
}
