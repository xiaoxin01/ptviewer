using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PtViewer.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("subscribes")]
        public string[] Subscribes { get; set; }
    }
}
