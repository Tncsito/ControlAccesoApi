using ControlAccesoApi.Servicios;
using MongoDB.Driver;

namespace ControlAccesoApi.Repositorios
{
    public class UsuarioRepositorio
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepositorio(MongoDBService mongoDBService)
        {
            _usuarios = mongoDBService.GetCollection<Usuario>("Usuarios");
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await _usuarios.Find(usuario => true).ToListAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(string id)
        {
            return await _usuarios.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefaultAsync();
        }

        public async Task CrearAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task ActualizarAsync(string id, Usuario usuarioActualizado)
        {
            await _usuarios.ReplaceOneAsync(usuario => usuario.Id == id, usuarioActualizado);
        }

        public async Task EliminarAsync(string id)
        {
            await _usuarios.DeleteOneAsync(usuario => usuario.Id == id);
        }
    }
}
