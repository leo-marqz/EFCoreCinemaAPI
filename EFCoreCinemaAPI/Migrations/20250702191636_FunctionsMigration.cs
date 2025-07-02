using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class FunctionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION dbo.GetInvoiceTotal(@invoiceId INT)
                RETURNS DECIMAL(18, 2)
                AS
                BEGIN
                    DECLARE @total DECIMAL(18, 2);
                    SELECT @total = SUM(Price) 
                    FROM InvoiceDetails 
                    WHERE InvoiceId = @invoiceId;
                    
                    RETURN ISNULL(@total, 0);
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP FUNCTION dbo.GetInvoiceTotal
            ");
        }
    }
}
