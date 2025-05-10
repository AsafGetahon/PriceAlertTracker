using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementTask.Common.Extensions
{
    public static class CurrencyExtensions
    {
        private static readonly Dictionary<string, decimal> ConversionRatesToILS = new()
    {
        { "USD", 3.6M }, // This is just an Example!! need to be updated constantly. for now - 1 USD = 3.6 ILS
        { "EUR", 3.9M }  // Same As USD: 1 EUR = 3.9 ILS
    };

        public static decimal ToILS(this decimal amount, string currency)
        {
            if (ConversionRatesToILS.TryGetValue(currency.ToUpper(), out var rate))
            {
                return Math.Round(amount * rate, 2);
            }

            throw new ArgumentException($"Unsupported currency: {currency}");
        }
    }
}
