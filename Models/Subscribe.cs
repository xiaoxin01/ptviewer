using System;
using MongoDB.Bson.Serialization.Attributes;

namespace PtViewer.Models
{
    [BsonIgnoreExtraElements]
    public class Subscribe : BaseModel
    {
        [BsonElement("_id")]
        public string Tag { get; set; }


        [BsonElement("subscribers")]
        public string[] Subscribers { get; set; }
    }
}
