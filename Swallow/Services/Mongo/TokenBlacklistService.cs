using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swallow.Models;

namespace Swallow.Services;

    public class TokenBlacklistService
    {
        // private readonly IMongoCollection<TokenBlacklist> _tokenBlacklistCollection;

        // public TokenBlacklistService(IOptions<SwallowDatabaseSettings> swallowDatabaseSettings)
        // {
        //     var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("DATABASES__CONNECTIONSTRING_MONGO"));
        //     var mongoDatabase = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASES__MAIN__DATABASENAME")); 

        //     _tokenBlacklistCollection = mongoDatabase.GetCollection<TokenBlacklist>(swallowDatabaseSettings.Value.TokenBlacklistCollectionName);
        // }

        // public async Task<TokenBlacklist?> GetByTokenStringAsync(string token) => await _tokenBlacklistCollection.Find(x => x.Token == token).FirstOrDefaultAsync();

        // public async Task CreateAsync(TokenBlacklist newTokenBlacklist) => await _tokenBlacklistCollection.InsertOneAsync(newTokenBlacklist);
        // public async Task<bool> DoesTokenExistAsync(string token) => await _tokenBlacklistCollection.Find(x => x.Token == token).FirstOrDefaultAsync() == null ? false : true;
    }

