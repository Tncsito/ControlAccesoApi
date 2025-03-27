using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ControlAccesoApi.Modelos
{
    public class Puerta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nombre")]
        public string? Nombre { get; set; }

        [BsonElement("Ubicacion")]
        public string? Ubicacion { get; set; }
    }
    public class PuertaDto
    {
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
    }
}
