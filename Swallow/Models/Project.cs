using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swallow.Models;

    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ExpectedEnd { get; set; }

        public List<String>? MemberList { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? GroupId { get; set; }
        
    }

