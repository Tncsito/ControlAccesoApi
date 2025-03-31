using ControlAccesoApi.Modelos;
using ControlAccesoApi.Servicios;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Repositorios
{
    public class PermisosRepositorio
    {
        private readonly IMongoCollection<Permisos> _permisos;

        public PermisosRepositorio(MongoDBService mongoDBService)
        {
            _permisos = mongoDBService.GetCollection<Permisos>("Permisos");
        }

        public async Task<List<Permisos>> ObtenerTodosAsync()
        {
            return await _permisos.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Permisos> ObtenerPorIdAsync(string id)
        {
            return await _permisos.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertarAsync(Permisos permiso)
        {
            await _permisos.InsertOneAsync(permiso);
        }

        public async Task ActualizarAsync(string id, Permisos permiso)
        {
            await _permisos.ReplaceOneAsync(p => p.Id == id, permiso);
        }

        public async Task EliminarAsync(string id)
        {
            await _permisos.DeleteOneAsync(p => p.Id == id);
        }
    }
}