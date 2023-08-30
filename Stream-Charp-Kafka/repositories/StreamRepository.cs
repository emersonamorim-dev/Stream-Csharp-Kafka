using MongoDB.Driver;
using Stream_Csharp_Kafka.models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Stream_Csharp_Kafka.repositories
{
    public class StreamRepository
    {
        private readonly IMongoCollection<StreamModel> _collection;

        public StreamRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDb:DatabaseName"]);
            _collection = database.GetCollection<StreamModel>("streams");
        }

        public async Task Create(StreamModel model)
        {
            await _collection.InsertOneAsync(model);
        }

        public async Task<StreamModel> Get(string id)
        {
            return await _collection.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StreamModel>> GetAll()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<StreamModel> Update(string id, StreamModel updatedModel)
        {
            await _collection.ReplaceOneAsync(new BsonDocument("_id", id), updatedModel);
            return updatedModel;
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(new BsonDocument("_id", id));
        }
    }
}

