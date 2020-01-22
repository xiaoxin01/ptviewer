using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PtViewer.Models
{
    [BsonIgnoreExtraElements]
    public class Item
    {
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("img_url")]
        [BsonIgnoreIfNull]
        public string Image { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("move_score")]
        [BsonIgnoreIfNull]
        public string MovieScore { get; set; }

        [BsonElement("move_url")]
        [BsonIgnoreIfNull]
        public string MovieUrl { get; set; }

        [BsonElement("size")]
        public string Size { get; set; }

        [BsonElement("source")]
        public string Source { get; set; }

        [BsonElement("created")]
        public string Created { get; set; }

        [BsonElement("favorators")]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public int[] Favorators { get; set; }

        [BsonIgnore]
        public bool Favorated { get => Favorators != null; }
    }
}
