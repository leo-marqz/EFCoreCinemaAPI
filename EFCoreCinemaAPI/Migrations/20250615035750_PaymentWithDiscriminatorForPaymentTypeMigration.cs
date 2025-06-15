using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class PaymentWithDiscriminatorForPaymentTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "date", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Digits = table.Column<string>(type: "char(4)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 1, 9.99m, new DateTime(2025, 6, 13, 21, 57, 49, 649, DateTimeKind.Local).AddTicks(4223), 0 },
                    { 2, 19.99m, new DateTime(2025, 6, 14, 11, 57, 49, 649, DateTimeKind.Local).AddTicks(4249), 0 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "Email", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 3, 99.99m, "leomarqz@gmail.com", new DateTime(2025, 6, 13, 21, 57, 49, 649, DateTimeKind.Local).AddTicks(8172), 2 },
                    { 4, 49.99m, "leomarqz@gmail.com", new DateTime(2025, 6, 14, 16, 57, 49, 649, DateTimeKind.Local).AddTicks(8183), 2 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "Digits", "TransactionDate", "Type" },
                values: new object[,]
                {
                    { 5, 79.99m, "1234", new DateTime(2025, 6, 12, 21, 57, 49, 644, DateTimeKind.Local).AddTicks(5403), 1 },
                    { 6, 29.99m, "5678", new DateTime(2025, 6, 14, 18, 57, 49, 644, DateTimeKind.Local).AddTicks(5427), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
