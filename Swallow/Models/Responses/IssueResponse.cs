using System;

namespace Swallow.Models.Responses
{
    public class IssueResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }
        
        public int CreatorId { get; set; }

        public int AssignedId { get; set; }

        public int ProjectId { get; set; }

        public int Weight { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public int TimeTaken { get; set; }

        public int Sprint { get; set; }
    }
}
