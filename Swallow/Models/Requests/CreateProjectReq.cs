using System;
using System.ComponentModel.DataAnnotations;

namespace Swallow.Models.Requests;    
    
    public class CreateProjectReq
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public int CreatorId { get; set; }
        
        public DateTime? ExpectedEnd { get; set; }
    }
