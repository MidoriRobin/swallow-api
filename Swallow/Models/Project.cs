using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swallow.Models;

    public class Project
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
        public string OwnerId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ExpectedEnd { get; set; }

        public List<String>? MemberList { get; set; }
        public string? GroupId { get; set; }
        
    }

