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
public class UsuarioDto
{
    public string? Nombre { get; set; }
    public int? Pin { get; set; }
    public string? Rol { get; set; }
    public DateTime? UltimoAcceso { get; set; }
}
