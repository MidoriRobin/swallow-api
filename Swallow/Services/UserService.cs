using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swallow.Models;

namespace Swallow.Services
{
    public class UserService
    {
        //* Implement mongo user collection
        private readonly IMongoCollection<User> _usersCollection;

        //* Add user service constructor and inject settings
        public UserService(IOptions<SwallowDatabaseSettings> swallowDatabaseSettings)
        {
            //Database setup 
            // var mongoClient = new MongoClient(swallowDatabaseSettings.Value.ConnectionString);
            // var mongoDatabase = mongoClient.GetDatabase(swallowDatabaseSettings.Value.DatabaseName);

            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("DATABASES__CONNECTIONSTRING"));
            var mongoDatabase = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASES__MAIN__DATABASENAME"));

            // User collection object
            _usersCollection = mongoDatabase.GetCollection<User>(swallowDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) => await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}