using BitcoinCurrency.Data.Entities;

namespace BitcoinCurrency.Data.IRepository
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task InsertAsync(T obj);
    }
}
