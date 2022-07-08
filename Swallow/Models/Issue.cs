using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Swallow.Util;

namespace Swallow.Models
{
    public class Issue
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }
        public ObjectId? CreatorId { get; set; }
        public ObjectId? AssignedId { get; set; }
        public ObjectId? ProjectId { get; set; }

        public int Weight { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public int TimeTaken { get; set; }

        public int Sprint { get; set; }

        
    }

}
