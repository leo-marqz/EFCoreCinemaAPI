using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            //Evalua en base a un campo que tipo de pago se usara
            builder.HasDiscriminator(pm=>pm.Type)
                .HasValue<Payment>(PaymentType.Cash) // esto es si la clase payment no es abstracta
                .HasValue<PaypalPay>(PaymentType.PayPal)
                .HasValue<CardPay>(PaymentType.Card);

            builder.Property(pm=>pm.Amount)
                .HasPrecision(18, 2); // Precision for decimal amounts

            var pay1 = new Payment
            {
                Id = 1,
                Amount = 9.99m,
                TransactionDate = DateTime.Now.AddDays(-1),
                Type = PaymentType.Cash
            };

            var pay2 = new Payment
            {
                Id = 2,
                Amount = 19.99m,
                TransactionDate = DateTime.Now.AddHours(-10),
                Type = PaymentType.Cash
            };

            builder.HasData(pay1, pay2);
        }
    }
}
