namespace BitcoinCurrency.Data.IRepository
{
    public interface IBitcoinCurrencyRepository : IRepositoryBase<BitcoinCurrency.Data.Models.BitcoinCurrency>
    {
        Task AddBitcoinCurrency(BitcoinCurrency.Data.Models.BitcoinCurrency currency);
    }
}
