using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nombre")]
    public string? Nombre { get; set; }

    [BsonElement("Pin")]
    public int Pin { get; set; }

    [BsonElement("Rol")]
    public string? Rol { get; set; }

    [BsonElement("UltimoAcceso")]
    public DateTime UltimoAcceso { get; set; }
}