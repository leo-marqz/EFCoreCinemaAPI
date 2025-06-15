using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class CardPayConfiguration : IEntityTypeConfiguration<CardPay>
    {
        public void Configure(EntityTypeBuilder<CardPay> builder)
        {
            builder.Property(cp=>cp.Digits)
                .HasColumnType("char(4)")
                .IsRequired();

            var cardPay1 = new CardPay
            {
                Id = 5,
                Amount = 79.99m,
                Type = PaymentType.Card,
                Digits = "1234",
                TransactionDate = DateTime.Now.AddDays(-2) // Example date, adjust as needed
            };

            var cardPay2 = new CardPay
            {
                Id = 6,
                Amount = 29.99m,
                Type = PaymentType.Card,
                Digits = "5678",
                TransactionDate = DateTime.Now.AddHours(-3) // Example date, adjust as needed
            };

            builder.HasData(cardPay1, cardPay2);
        }
    }
}
