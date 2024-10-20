using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecurePrivacy.Task1.API.Resources.Consent;

public class Consent
{
    public Consent(string? id, string clientId, List<ConsentDetail> consentDetails)
    {
        Id = id;
        ClientId = clientId;
        ConsentDetails = consentDetails;
    }

    public Consent(string clientId, List<ConsentDetail> consentDetails)
    {
        ClientId = clientId;
        ConsentDetails = consentDetails;
    }

    public Consent() {}

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string ClientId { get; set; }
    public List<ConsentDetail> ConsentDetails { get; set; }
}

public class ConsentDetail
{
    public ConsentDetail(string type, bool consented)
    {
        Type = type;
        Consented = consented;
    }

    public string Type { get; private set; }
    public bool Consented { get; private set; }
}