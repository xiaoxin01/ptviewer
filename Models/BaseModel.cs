using System;
using MongoDB.Bson.Serialization.Attributes;

public class BaseModel
{
    protected BaseModel()
    {
        this.Created = DateTime.Now;
    }

    [BsonElement("created")]
    public DateTime? Created { get; set; }

    [BsonElement("modified")]
    public DateTime? Modified { get; set; }
}
