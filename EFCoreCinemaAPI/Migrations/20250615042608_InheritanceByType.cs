using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InheritanceByType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RAM = table.Column<int>(type: "int", nullable: false),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    GraphicsCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenSize = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laptops_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merchandising",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsCollectible = table.Column<bool>(type: "bit", nullable: false),
                    IsClothes = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchandising", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchandising_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 13, 22, 26, 8, 408, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 12, 26, 8, 408, DateTimeKind.Local).AddTicks(6462));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 13, 22, 26, 8, 409, DateTimeKind.Local).AddTicks(526));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 17, 26, 8, 409, DateTimeKind.Local).AddTicks(537));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 12, 22, 26, 8, 399, DateTimeKind.Local).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 19, 26, 8, 399, DateTimeKind.Local).AddTicks(4448));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, 799.99m },
                    { 2, "Cine T-Shirt", 19.99m }
                });

            migrationBuilder.InsertData(
                table: "Laptops",
                columns: new[] { "Id", "Brand", "GraphicsCard", "Model", "Processor", "RAM", "ScreenSize", "Storage" },
                values: new object[] { 1, "HP", null, "Pavilion 15", "Intel Core i7", 16, 0.0, 512 });

            migrationBuilder.InsertData(
                table: "Merchandising",
                columns: new[] { "Id", "IsAvailable", "IsClothes", "IsCollectible", "Volume", "Weight" },
                values: new object[] { 2, true, false, true, 0.5, 0.20000000000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "Merchandising");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 13, 21, 57, 49, 649, DateTimeKind.Local).AddTicks(4223));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 11, 57, 49, 649, DateTimeKind.Local).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 13, 21, 57, 49, 649, DateTimeKind.Local).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 16, 57, 49, 649, DateTimeKind.Local).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 12, 21, 57, 49, 644, DateTimeKind.Local).AddTicks(5403));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                column: "TransactionDate",
                value: new DateTime(2025, 6, 14, 18, 57, 49, 644, DateTimeKind.Local).AddTicks(5427));
        }
    }
}
