using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stream_Csharp_Kafka.models
{
    public class StreamModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}


