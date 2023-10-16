using BitcoinCurrency.Data.Entities;

namespace BitcoinCurrency.Data.Models
{
    public class BitcoinCurrency : BaseEntity
    {
        public string currencyReal { get; set; } = string.Empty;

        public string currencyUsd { get; set; } = string.Empty;

        public string realValueToUsd { get; set; } = string.Empty;

        public DateTime updateDate { get; set; }
    }
}
