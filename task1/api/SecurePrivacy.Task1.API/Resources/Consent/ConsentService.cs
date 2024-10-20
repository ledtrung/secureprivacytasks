using MongoDB.Driver;

namespace SecurePrivacy.Task1.API.Resources.Consent;

public class ConsentService
{
    private readonly IMongoCollection<Consent> _consentCollection;

    public ConsentService(IMongoCollection<Consent> consentCollection)
    {
        _consentCollection = consentCollection;
    }

    /// <summary>
    /// Get all consent types
    /// </summary>
    /// <returns></returns>
    public async Task<List<ConsentType>> GetConsentTypes()
    {
        return await Task.FromResult(new List<ConsentType>()
        {
            new ConsentType("Essential", true),
            new ConsentType("Analytics", false),
            new ConsentType("Advertising", false)
        });
    }

    /// <summary>
    /// Get client consent
    /// </summary>
    /// <param name="id">Identity of the client</param>
    /// <returns></returns>
    public async Task<Consent> GetConsent(string id)
    {
        return await _consentCollection.Find(e => e.ClientId == id).FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// Add/edit consent for a client
    /// </summary>
    /// <param name="consent">Consent details</param>
    public async Task AddOrUpdateConsent(Consent consent)
    {
        var filter = Builders<Consent>.Filter.Eq(e => e.ClientId, consent.ClientId);
        var update = Builders<Consent>.Update.Set(e => e.ConsentDetails, consent.ConsentDetails);
        await _consentCollection.UpdateOneAsync(filter, update, new UpdateOptions() { IsUpsert = true });
    }
}