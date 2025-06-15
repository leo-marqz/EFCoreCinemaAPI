using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsAuditableEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Genres",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Genres",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 13, 58, 48, 938, DateTimeKind.Local).AddTicks(5468));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 3, 58, 48, 938, DateTimeKind.Local).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 13, 58, 48, 938, DateTimeKind.Local).AddTicks(9216));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 8, 58, 48, 938, DateTimeKind.Local).AddTicks(9225));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 13, 13, 58, 48, 924, DateTimeKind.Local).AddTicks(2808));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 10, 58, 48, 924, DateTimeKind.Local).AddTicks(2828));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Genres");

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 13, 4, 40, 839, DateTimeKind.Local).AddTicks(2260));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 3, 4, 40, 839, DateTimeKind.Local).AddTicks(2273));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 13, 4, 40, 839, DateTimeKind.Local).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 8, 4, 40, 839, DateTimeKind.Local).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 13, 13, 4, 40, 834, DateTimeKind.Local).AddTicks(2527));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 15, 10, 4, 40, 834, DateTimeKind.Local).AddTicks(2545));
        }
    }
}
