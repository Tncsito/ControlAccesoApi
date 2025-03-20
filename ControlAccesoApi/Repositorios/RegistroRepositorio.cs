using ControlAccesoApi.Modelos;
using ControlAccesoApi.Servicios;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Repositorios
{
    public class RegistroRepositorio
    {
        private readonly IMongoCollection<Registro> _registros;

        public RegistroRepositorio(MongoDBService mongoDBService)
        {
            _registros = mongoDBService.GetCollection<Registro>("Registros");
        }

        public async Task<List<Registro>> ObtenerTodosAsync()
        {
            return await _registros.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Registro> ObtenerPorIdAsync(string id)
        {
            return await _registros.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertarAsync(Registro registro)
        {
            await _registros.InsertOneAsync(registro);
        }

        public async Task ActualizarAsync(string id, Registro registro)
        {
            await _registros.ReplaceOneAsync(r => r.Id == id, registro);
        }

        public async Task EliminarAsync(string id)
        {
            await _registros.DeleteOneAsync(r => r.Id == id);
        }
    }
}
