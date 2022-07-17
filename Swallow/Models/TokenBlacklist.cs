using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swallow.Models;

    public class TokenBlacklist
    {

        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime? EntryDate { get; set; }
    }

