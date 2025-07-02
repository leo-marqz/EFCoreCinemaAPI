using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CalculatedTotalInvoiceDetailMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "InvoiceDetails",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                computedColumnSql: "[Price] * [Quantity]");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceDetails");

        }
    }
}
