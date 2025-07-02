using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI.Models.Functions
{
    public static class Scalars
    {
        public static void RegisterFunctions(ModelBuilder modelBuilder)
        {
            // Register scalar functions here
            modelBuilder.HasDbFunction(() => GetInvoiceTotal(default));
        }

        public static decimal GetInvoiceTotal(int invoiceId){ return 0; }
    }
}
