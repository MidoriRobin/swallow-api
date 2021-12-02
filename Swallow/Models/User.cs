using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swallow.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string Email {get; set;}

    public string Username {get; set;}

    public string Password { get; set; }

    public string? Occupation { get; set; }

    public bool IsAdmin { get; set; }
}