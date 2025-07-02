using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "TransactionDate" },
                values: new object[] { 3, new DateTime(2025, 7, 2, 13, 16, 9, 274, DateTimeKind.Local).AddTicks(749) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 1, 13, 16, 9, 282, DateTimeKind.Local).AddTicks(5176));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 2, 3, 16, 9, 282, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 1, 13, 16, 9, 282, DateTimeKind.Local).AddTicks(9237));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 2, 8, 16, 9, 282, DateTimeKind.Local).AddTicks(9245));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 30, 13, 16, 9, 275, DateTimeKind.Local).AddTicks(9748));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 2, 10, 16, 9, 275, DateTimeKind.Local).AddTicks(9765));

            migrationBuilder.InsertData(
                table: "InvoiceDetails",
                columns: new[] { "Id", "InvoiceId", "Price", "Product" },
                values: new object[,]
                {
                    { 3, 3, 15m, "Product C" },
                    { 4, 3, 25m, "Product D" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "CineOffers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 30, 13, 1, 49, 138, DateTimeKind.Local).AddTicks(6247));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 1, 3, 1, 49, 138, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 30, 13, 1, 49, 138, DateTimeKind.Local).AddTicks(9677));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 1, 8, 1, 49, 138, DateTimeKind.Local).AddTicks(9687));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 29, 13, 1, 49, 133, DateTimeKind.Local).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 7, 1, 10, 1, 49, 133, DateTimeKind.Local).AddTicks(454));
        }
    }
}
