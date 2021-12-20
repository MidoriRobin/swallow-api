using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Swallow.Util;

namespace Swallow.Models
{
    public class Issue
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }
    
        
        [JsonConverter(typeof(StringToObjectId))]
        // [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? CreatorId { get; set; }

        
        [JsonConverter(typeof(StringToObjectId))]
        // [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? AssignedId { get; set; }

        
        [JsonConverter(typeof(StringToObjectId))]
        // [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? ProjectId { get; set; }

        public int Weight { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public int TimeTaken { get; set; }

        public int Sprint { get; set; }

        
    }

    enum IssueStatus
    {
        Todo = 1,
        InPro = 2,
        Paused = 3,
        Completed = 4,
        Abandoned = 5
    }
}
