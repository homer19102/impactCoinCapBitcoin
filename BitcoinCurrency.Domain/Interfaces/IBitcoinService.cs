using BitcoinCurrency.Domain.Models;

namespace BitcoinCurrency.Domain
{
    public interface IBitcoinService
    {
        Task<CurrencyModelView> ExecuteBitcoinCurrency();
    }
}
