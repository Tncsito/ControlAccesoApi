using MongoDB.Bson.Serialization.Attributes;

namespace ControlAccesoApi.Modelos
{
    public class Circuitos
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Permiso_id")]
        public string? PermisoId { get; set; }
        [BsonElement("Estado")]
        public bool? Estado { get; set; }
        [BsonElement("Fecha")]
        public DateTime? Fecha { get; set; }
    }
    public class CircuitosDto
    {
        public string? PermisoId { get; set; }
        public bool? Estado { get; set; }
        public DateTime? Fecha { get; set; }

    }
}
