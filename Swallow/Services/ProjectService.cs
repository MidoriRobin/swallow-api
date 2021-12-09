using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swallow.Models;

namespace Swallow.Services;

    public class ProjectService
    {
        
        private readonly IMongoCollection<Project> _projectCollection;

        public ProjectService(IOptions<SwallowDatabaseSettings> swallowDatabaseSettings)
        {
            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("DATABASES__CONNECTIONSTRING"));
            var mongoDatabase = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASES__MAIN__DATABASENAME"));

            _projectCollection = mongoDatabase.GetCollection<Project>(swallowDatabaseSettings.Value.ProjectCollectionName);
        }

        // Getters
        public async Task<List<Project>> GetProjectsAsync() => await _projectCollection.Find(_ => true).ToListAsync();

        public async Task<Project?> GetProjectAsync(string id) => await _projectCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Project>> GetProjectsByOwnerAsync(string ownerId) => await _projectCollection.Find(x => x.OwnerId == ownerId).ToListAsync();

        public async Task<List<Project>> GetProjectsByGroupIdAsync(string groupId) => await _projectCollection.Find(x => x.GroupId == groupId).ToListAsync();

        public async Task<List<Project>> GetProjectsByMemberIdAsync(string memberId) => await _projectCollection.Find(x => x.MemberList.Contains(memberId)).ToListAsync();

        public async Task<List<Project>> GetProjectsInRangeAsync(DateTime start, DateTime end) => await _projectCollection.Find(x => x.CreatedDate.CompareTo(start) >= 0 && x.CreatedDate.CompareTo(end) <= 0).ToListAsync();

        public async Task<List<Project>> GetProjectsByTypeAsync(string type) => await _projectCollection.Find(x => x.Type == type).ToListAsync();

        // Setters
        public async Task CreateAsync(Project newProject) => await _projectCollection.InsertOneAsync(newProject);
        
        public async Task UpdateAsync(string id, Project updatedProject) => await _projectCollection.ReplaceOneAsync(x => x.Id == id, updatedProject);

        public async Task RemoveAsync(string id) => await _projectCollection.DeleteOneAsync(x => x.Id == id);

    }

