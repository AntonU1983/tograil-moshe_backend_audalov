using System;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GenericBackend.Core
{
    /// <summary>
    /// "Default" Entity interface.
    /// </summary>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public interface IMongoEntity : IMongoEntity<string>
    {

    }

    public class MongoEntityBase : IMongoEntity
    {
        public MongoEntityBase()
        {
            Id = Guid.NewGuid().ToString("D");
        }

        public string Id { get; set; }
        public string LandingId { get; set; } = "unknown";
    }
    public interface IMongoEntity<TKey> : IEntity<TKey>
        where TKey: IComparable
    {
        /// <summary>
        /// Gets or sets the Id of the Entity.
        /// </summary>
        /// <value>Id of the Entity.</value>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        new TKey Id { get; set; }
    }
}