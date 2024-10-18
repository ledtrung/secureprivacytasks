using MongoDB.Driver;

namespace SecurePrivacy.Task1.API.Resources.Users;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(IMongoCollection<User> userCollection)
    {
        _userCollection = userCollection;
    }

    /// <summary>
    /// Get all users that satisfy the filter
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetUsers()
    {
        return await _userCollection.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Get user information
    /// </summary>
    /// <param name="id">Identity of the user</param>
    /// <returns></returns>
    public async Task<User?> GetUser(string id)
    {
        return await _userCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// Insert a new user. Will throw exception if trying to update an existing user here.
    /// </summary>
    /// <param name="user">User information</param>
    public async Task AddUser(User user)
    {
        await _userCollection.InsertOneAsync(user);
    }
}