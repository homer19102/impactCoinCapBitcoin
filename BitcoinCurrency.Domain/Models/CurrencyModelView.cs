namespace BitcoinCurrency.Domain.Models
{
    public class CurrencyModelView
    {
        public string currencyReal { get; set; } = string.Empty;
        public string currencyUsd { get; set; } = string.Empty;

        public string valueUseToConvertUsdToReal { get; set; } = string.Empty;
    }
}
