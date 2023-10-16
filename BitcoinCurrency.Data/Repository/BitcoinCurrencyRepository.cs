using BitcoinCurrency.Data.IRepository;
using MongoDB.Driver;

namespace BitcoinCurrency.Data.Repository
{
    public class BitcoinCurrencyRepository : RepositoryBase<BitcoinCurrency.Data.Models.BitcoinCurrency>, IBitcoinCurrencyRepository
    {
        public BitcoinCurrencyRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "BitcoinCurrency")
        {
        }

        public async Task AddBitcoinCurrency(BitcoinCurrency.Data.Models.BitcoinCurrency currency)
        {
            await Collection.InsertOneAsync(currency);
        }
    }
}
