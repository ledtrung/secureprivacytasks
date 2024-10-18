
using MongoDB.Driver;
using SecurePrivacy.Task1.API.Resources.Users;

public class UserStoreIndexesHostedService : IHostedService
{
    private readonly IMongoCollection<User> _userCollection;
    private readonly ILogger<UserStoreIndexesHostedService> _logger;

    public UserStoreIndexesHostedService(IMongoCollection<User> userCollection, ILogger<UserStoreIndexesHostedService> logger)
    {
        _userCollection = userCollection;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a test compound index for Name and Dob");
        var indexModel = new CreateIndexModel<User>(Builders<User>
            .IndexKeys
            .Ascending(u => u.Name)
            .Ascending(u => u.Dob));
        //We don't wait for this execution so our API endpoints can be served immediately without waiting for the indexing to be finished
        _userCollection.Indexes.CreateOneAsync(indexModel);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}