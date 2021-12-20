using System;
using MongoDB.Bson;

namespace Swallow.Models.DTOs;

    public class IssuesDTO
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }
    
        public string? CreatorId { get; set; }

        public string? AssignedId { get; set; }

        public string? ProjectId { get; set; }

        public int Weight { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public int TimeTaken { get; set; }

        public int Sprint { get; set; }

        public Issue ConvertToIssue() 
        {
            Issue issue = new Issue()
            { 
                Name = this.Name, 
                Type = this.Type, 
                Status = this.Status,
                Description = this.Description,
                CreatorId = ObjectId.Parse(this.CreatorId),
                AssignedId = this.AssignedId is null ? ObjectId.Parse(this.CreatorId) : ObjectId.Parse(this.AssignedId),
                ProjectId = ObjectId.Parse(this.ProjectId),
                Weight = this.Weight,
                CreatedDate = this.CreatedDate,
                CompletedDate = this.CompletedDate,
                TimeTaken = this.TimeTaken,
                Sprint = this.Sprint,
            };

            return issue;
        }
    }


