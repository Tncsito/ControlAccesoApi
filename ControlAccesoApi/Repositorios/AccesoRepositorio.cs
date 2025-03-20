using ControlAccesoApi.Modelos;
using ControlAccesoApi.Servicios;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Repositorios
{
    public class AccesoRepositorio
    {
        private readonly IMongoCollection<Accesos> _accesos;

        public AccesoRepositorio(MongoDBService mongoDBService)
        {
            _accesos = mongoDBService.GetCollection<Accesos>("Accesos");
        }

        public async Task<List<Accesos>> ObtenerTodosAsync()
        {
            return await _accesos.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Accesos> ObtenerPorIdAsync(string id)
        {
            return await _accesos.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertarAsync(Accesos acceso)
        {
            await _accesos.InsertOneAsync(acceso);
        }

        public async Task ActualizarAsync(string id, Accesos acceso)
        {
            await _accesos.ReplaceOneAsync(a => a.Id == id, acceso);
        }

        public async Task EliminarAsync(string id)
        {
            await _accesos.DeleteOneAsync(a => a.Id == id);
        }
    }
}
