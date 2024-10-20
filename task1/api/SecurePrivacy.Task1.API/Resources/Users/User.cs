using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecurePrivacy.Task1.API.Resources.Users;

public class User
{
    public User(string? id, string name, string email, string address, DateTime dob)
    {
        Id = id;
        Name = name;
        Email = email;
        Address = address;
        Dob = dob;
    }

    public User(string name, string email, string address, DateTime dob)
    {
        Name = name;
        Email = email;
        Address = address;
        Dob = dob;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    [BsonElement("name")]
    public string Name { get; private set; }
    [BsonElement("email")]
    public string Email { get; private set; }
    [BsonElement("address")]
    public string Address { get; private set; }
    [BsonElement("dob")]
    public DateTime Dob { get; private set; }
}

public class UserFilter
{ 
    public string? NameStartWith { get; set; }
    internal bool HasNameFilter => !string.IsNullOrWhiteSpace(NameStartWith);
    public DateTime? BornBefore { get; set; }
    internal bool HasBornBeforeFilter => BornBefore.HasValue;
    public DateTime? BornAfter { get; set; }
    internal bool HasBornAfterFilter => BornAfter.HasValue;
    
    internal bool HasFilter => HasNameFilter || HasBornBeforeFilter || HasBornAfterFilter;
}