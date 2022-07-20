using System;

namespace Swallow.Models.Responses;
    public class ProjectResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpectedEnd { get; set; }
    }
