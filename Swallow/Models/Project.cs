using System;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swallow.Models;

// TODO: Create migration
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public User User { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ExpectedEnd { get; set; }

        // public int MemberId { get; set; }
        // public int GroupId { get; set; }

        public List<Issue> Issues { get; set; }
        
    }

