using MongoDB.Bson.Serialization.Attributes;

namespace PtViewer.Models
{
    [BsonIgnoreExtraElements]
    public class Subscribe
    {
        [BsonElement("_id")]
        public string Tag { get; set; }


        [BsonElement("subscribers")]
        public string[] SubscriberS { get; set; }
    }
}
