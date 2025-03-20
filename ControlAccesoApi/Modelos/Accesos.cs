using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ControlAccesoApi.Modelos
{
    public class Accesos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Usuario_id")]
        public string? UsuarioId { get; set; }

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("Metodo")]
        public string? Metodo { get; set; }

        [BsonElement("Estado")]
        public bool Estado { get; set; }
    }
}
