using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace ControlAccesoApi.Services
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(IConfiguration configuration)
        {
            var connectionString = configuration["mongodb+srv://dye:<db_password>@control.nmu0r.mongodb.net/?retryWrites=true&w=majority&appName=Control"];
            var databaseName = configuration["MongoDB:Control"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
