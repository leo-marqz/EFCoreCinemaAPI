using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class PaypalPayConfiguration : IEntityTypeConfiguration<PaypalPay>
    {
        public void Configure(EntityTypeBuilder<PaypalPay> builder)
        {
            builder.Property(p => p.Email)
                    .HasMaxLength(150)
                    .IsRequired();

            var paypalPay1 = new PaypalPay
            {
                Id = 3,
                Amount = 99.99m,
                Type = PaymentType.PayPal,
                Email = "leomarqz@gmail.com",
                TransactionDate = DateTime.Now.AddDays(-1) // Example date, adjust as needed
            };

            var paypalPay2 = new PaypalPay
            {
                Id = 4,
                Amount = 49.99m,
                Type = PaymentType.PayPal,
                Email = "leomarqz@gmail.com",
                TransactionDate = DateTime.Now.AddHours(-5) // Example date, adjust as needed
            };

            builder.HasData(paypalPay1, paypalPay2);
        }
    } 
}
