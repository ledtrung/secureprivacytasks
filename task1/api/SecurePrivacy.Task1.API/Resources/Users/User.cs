using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SecurePrivacy.Task1.API.Resources.Users;

public class User
{
    public User(string? id, string name, string address, DateTime dob)
    {
        Id = id;
        Name = name;
        Address = address;
        Dob = dob;
    }

    public User(string name, string address, DateTime dob)
    {
        Name = name;
        Address = address;
        Dob = dob;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [BsonElement("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    [BsonElement("name")]
    public string Name { get; private set; }
    [BsonElement("address")]
    public string Address { get; private set; }
    [BsonElement("dob")]
    public DateTime Dob { get; private set; }
}

public class UserFilter
{ 
    public string? Name { get; set; }
}