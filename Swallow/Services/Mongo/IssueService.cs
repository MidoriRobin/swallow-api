using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Swallow.Models;

namespace Swallow.Services;
    public class IssueService
    {
        // private readonly IMongoCollection<Issue> _issueCollection;

        // public IssueService(IOptions<SwallowDatabaseSettings> swallowDatabaseSettings)
        // {
        //     var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("DATABASES__CONNECTIONSTRING_MONGO"));
        //     var mongoDatabase = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASES__MAIN__DATABASENAME"));

        //     _issueCollection = mongoDatabase.GetCollection<Issue>(swallowDatabaseSettings.Value.IssuesCollectionName);
        // }

        // public async Task<List<Issue>> GetIssuesAsync() => await _issueCollection.Find(_ => true).ToListAsync();

        // public async Task<Issue?> GetIssueAsync(string id) => await _issueCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // public async Task<List<Issue>> GetIssuesByProjectAsync(string projectId) => await _issueCollection.Find(x => x.ProjectId == ObjectId.Parse(projectId)).ToListAsync();

        // public async Task<List<Issue>> GetIssuesByAssignedIdAsync(string assignedUser) => await _issueCollection.Find(x => x.AssignedId.Equals(assignedUser)).ToListAsync();

        // public async Task<List<Issue>> GetIssuesByCreatorAsync(string creatorId) => await _issueCollection.Find(x => x.CreatorId.Equals(creatorId)).ToListAsync();

        // public async Task<List<Issue>> GetIssuesByProjectInDateRangeAsync(string id, DateTime start, DateTime end) => await _issueCollection.Find(x => x.ProjectId == ObjectId.Parse(id) && x.CreatedDate.CompareTo(start) >= 0 && x.CreatedDate.CompareTo(end) <= 0).ToListAsync();

        // // TODO: Get issues by type and group.
        
        // public async Task<List<Issue>> GetIssuesByTypeAsync(string type) => await _issueCollection.Find(x => x.Type == type).ToListAsync();
        
        // public async Task<List<Issue>> GetIssuesByTypeAndProjectIdAsync(string type, string projectId) => await _issueCollection.Find(x => x.Type == type && x.ProjectId.Equals(projectId)).ToListAsync();

        // public async Task<List<Issue>> GetIssuesByCreatorAndTypeAsync(string creatorId, string type) => await _issueCollection.Find(x => x.CreatorId.Equals(creatorId) && x.Type == type).ToListAsync();

        // public async Task<List<Issue>> GetIssuesByDateAndTypeAsync(string type, DateTime start, DateTime end) => await _issueCollection.Find(x => x.Type == type && x.CreatedDate.CompareTo(start) >= 0 && x.CreatedDate.CompareTo(end) <= 0).ToListAsync();

        // // Setters
        // public async Task CreateAsync(Issue newIssue) => await _issueCollection.InsertOneAsync(newIssue);
        // public async Task UpdateAsync(string id, Issue updatedIssue) => await _issueCollection.ReplaceOneAsync(x => x.Id == id, updatedIssue);
        // public async Task RemoveAsync(string id) => await _issueCollection.DeleteOneAsync(x => x.Id == id);

    }
