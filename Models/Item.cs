using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PtViewer.Models
{
    [BsonIgnoreExtraElements]
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("move_score")]
        public string MovieScore { get; set; }
        [BsonElement("move_url")]
        public string MovieUrl { get; set; }
        [BsonElement("size")]
        public string Size { get; set; }
        [BsonElement("source")]
        public string Source { get; set; }

        [BsonElement("created")]
        public string Created { get; set; }
    }
}
