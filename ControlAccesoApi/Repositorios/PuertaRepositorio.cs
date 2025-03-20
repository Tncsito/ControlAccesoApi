using ControlAccesoApi.Modelos;
using ControlAccesoApi.Servicios;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Repositorios
{
    public class PuertaRepositorio
    {
        private readonly IMongoCollection<Puerta> _puertas;

        public PuertaRepositorio(MongoDBService mongoDBService)
        {
            _puertas = mongoDBService.GetCollection<Puerta>("Puertas");
        }

        public async Task<List<Puerta>> ObtenerTodasAsync()
        {
            return await _puertas.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Puerta> ObtenerPorIdAsync(string id)
        {
            return await _puertas.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertarAsync(Puerta puerta)
        {
            await _puertas.InsertOneAsync(puerta);
        }

        public async Task ActualizarAsync(string id, Puerta puerta)
        {
            await _puertas.ReplaceOneAsync(p => p.Id == id, puerta);
        }

        public async Task EliminarAsync(string id)
        {
            await _puertas.DeleteOneAsync(p => p.Id == id);
        }
    }
}
