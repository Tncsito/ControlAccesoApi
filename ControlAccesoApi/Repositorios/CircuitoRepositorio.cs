using ControlAccesoApi.Modelos;
using ControlAccesoApi.Servicios;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Repositorios
{
    public class CircuitoRepositorio
    {
        private readonly IMongoCollection<Circuitos> _circuito;

        public CircuitoRepositorio(MongoDBService mongoDBService)
        {
            _circuito = mongoDBService.GetCollection<Circuitos>("Circuitos");
        }

        public async Task<List<Circuitos>> ObtenerTodasAsync()
        {
            return await _circuito.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Circuitos> ObtenerPorIdAsync(string id)
        {
            return await _circuito.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertarAsync(Circuitos circuito)
        {
            await _circuito.InsertOneAsync(circuito);
        }

        public async Task ActualizarAsync(string id, Circuitos circuito)
        {
            await _circuito.ReplaceOneAsync(p => p.Id == id, circuito);
        }

        public async Task EliminarAsync(string id)
        {
            await _circuito.DeleteOneAsync(p => p.Id == id);
        }
    }
}