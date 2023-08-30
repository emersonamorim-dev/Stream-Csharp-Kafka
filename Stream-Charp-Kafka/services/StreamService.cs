using Confluent.Kafka;
using Stream_Csharp_Kafka.models;
using Stream_Csharp_Kafka.repositories;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stream_Csharp_Kafka.services
{
    public class StreamService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly StreamRepository _repository;

        public StreamService(IProducer<Null, string> producer, StreamRepository repository)
        {
            _producer = producer;
            _repository = repository;
        }

        public async Task CreateAndPublish(StreamModel model)
        {
            await _repository.Create(model);
            await _producer.ProduceAsync("create_topic", new Message<Null, string> { Value = JsonSerializer.Serialize(model) });
        }

        public async Task<StreamModel> GetAndConsume(string id)
        {
            var model = await _repository.Get(id);
            if (model != null)
            {
                await _producer.ProduceAsync("get_topic", new Message<Null, string> { Value = JsonSerializer.Serialize(model) });
            }
            return model;
        }

        public async Task<IEnumerable<StreamModel>> GetAllAndConsume()
        {
            var models = await _repository.GetAll();
            foreach (var model in models)
            {
                await _producer.ProduceAsync("getall_topic", new Message<Null, string> { Value = JsonSerializer.Serialize(model) });
            }
            return models;
        }

        public async Task<StreamModel> UpdateAndPublish(string id, StreamModel updatedModel)
        {
            var model = await _repository.Update(id, updatedModel);
            if (model != null)
            {
                await _producer.ProduceAsync("update_topic", new Message<Null, string> { Value = JsonSerializer.Serialize(model) });
            }
            return model;
        }

        public async Task DeleteAndPublish(string id)
        {
            await _repository.Delete(id);
            await _producer.ProduceAsync("delete_topic", new Message<Null, string> { Value = id });
        }
    }
}
