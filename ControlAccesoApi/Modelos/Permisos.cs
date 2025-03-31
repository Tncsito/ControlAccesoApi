using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControlAccesoApi.Modelos
{
    public class Permisos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Usuario_id")]
        public string? UsuarioId { get; set; }

        [BsonElement("Puertas_id")]
        public string? PuertasId { get; set; }

        [BsonElement("Puesto")]
        public string? Puesto { get; set; }

        [BsonElement("Dias")]
        public string? Dias { get; set; }

        [BsonElement("Horas")]
        public string? Horas { get; set; }
    }
    public class PermisosDto
    {
        public string? UsuarioId { get; set; }
        public string? PuertasId { get; set; }
        public string? Puesto { get; set; }
        public string? Dias { get; set; }
        public string? Horas { get; set; }
    }
}
