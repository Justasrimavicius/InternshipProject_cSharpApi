using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class Record {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string date { get; set; } = null!;
    // [BsonElement("items")]
    // [JsonPropertyName("items")]
    public int calories { get; set; }
    public int protein { get; set; }
    public int carbs { get; set; }
    public int fats { get; set; }
    public bool goalsAchieved { get; set; } = false!;

}