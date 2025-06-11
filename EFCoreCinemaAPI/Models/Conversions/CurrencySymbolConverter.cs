
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreCinemaAPI.Models.Conversions
{
    public class CurrencySymbolConverter : ValueConverter<Currency, string>
    {
        public CurrencySymbolConverter()
         : base(
               (value)=> ConvertToString(value),
               (value) => ConvertFromString(value)
               ) { }

        private static string ConvertToString(Currency currency)
        {
            return currency switch
            {
                Currency.USD => "$",
                Currency.EUR => "€",
                Currency.GBP => "£",
                Currency.JPY => "¥",
                Currency.CNY => "¥",
                Currency.INR => "₹",
                Currency.RUB => "₽",
                _ => string.Empty, // Default case for unknown currency
            };
        }

        private static Currency ConvertFromString(string symbol)
        {
            return symbol switch
            {
                "$" => Currency.USD,
                "€" => Currency.EUR,
                "£" => Currency.GBP,
                "¥" => Currency.JPY, // Assuming JPY and CNY share the same symbol
                "₹" => Currency.INR,
                "₽" => Currency.RUB,
                _ => Currency.Unknown, // Default case for unknown symbol
            };
        }
    }
}
