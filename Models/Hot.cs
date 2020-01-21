using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace PtViewer.Models
{
    [BsonIgnoreExtraElements]
    public class Hot
    {
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("created")]
        [JsonIgnore]
        public int CreatedUnix { get; set; }

        [BsonIgnore]
        public DateTime Created
        {
            get => DateTimeOffset.FromUnixTimeSeconds(CreatedUnix).DateTime;
        }
    }
}
