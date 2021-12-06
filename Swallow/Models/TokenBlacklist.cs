using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swallow.Models
{
    public class TokenBlacklist
    {
        // public TokenBlacklist(string token, DateTime entryDate, string belongsTo)
        // {
        //     this.Token = token;
        //     this.EntryDate = entryDate;
        //     this.BelongsTo = belongsTo;
        // }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Token { get; set; }
        public DateTime EntryDate { get; set; }
        public string BelongsTo { get; set; }
    }
}
