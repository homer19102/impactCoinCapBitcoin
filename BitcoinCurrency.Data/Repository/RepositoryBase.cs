using BitcoinCurrency.Data.Entities;
using BitcoinCurrency.Data.IRepository;
using MongoDB.Driver;

namespace BitcoinCurrency.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private const string DATABASE = "ImpactaEngenhariaDados";
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;

        public RepositoryBase(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
        {
            (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

            if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(DATABASE).CreateCollection(collection);
        }

        protected virtual IMongoCollection<T> Collection =>
      _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

        public async Task InsertAsync(T obj) =>
        await Collection.InsertOneAsync(_clientSessionHandle, obj);


    }
}

