using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BitcoinCurrency.Data.Entities
{
    public abstract class BaseEntity
    {
        [BsonElement("_id")]
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public virtual ObjectId Id { get; private set; }

        public void SetId(ObjectId id) =>
            Id = id;
    }
}
