using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swallow.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Net.Mail;
using MongoDB.Bson;

namespace Swallow.Services;

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

        public async Task<User?> GetByEmailAsync(string email) => await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) => await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);



        // Util functions-----

        public async Task<bool> EmailDoesExistAsync(string email) => await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync() != null ? true : false;
        

        public async Task<User> CredCheckAsync(string email, string password) => await _usersCollection.Find(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();


        public async Task<bool> ResetTokenDateAsync(string email)
        {
            DateTime nullDate = DateTime.UnixEpoch;

            var updateResult = await _usersCollection.FindOneAndUpdateAsync(
                x => x.Email == email, Builders<User>
                .Update.Set(x => x.TokenExpiry, nullDate));

            return updateResult == null ? false : true;
        }


        public async Task<Dictionary<string, string>> processRegistration(User newUser)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            bool processSuccess = false;
            bool isValidEmail = false;
            bool doesExist = false;
            string reason = "";

            try
            {
                MailAddress validMailAddress = new MailAddress(newUser.Email);
                isValidEmail = true;     
            }
            finally
            {
                doesExist = await EmailDoesExistAsync(newUser.Email);
            }


            if (isValidEmail && !doesExist)
            {
                newUser.Password = BCryptNet.HashPassword(newUser.Password);
                
                await CreateAsync(newUser);

                processSuccess = true;
            }else
            {
                reason = !isValidEmail ? "Invalid email" : "Email already exists";
            }

            result.Add("success", processSuccess.ToString());
            result.Add("reason", reason.ToString());

            return result;
        }
    }
