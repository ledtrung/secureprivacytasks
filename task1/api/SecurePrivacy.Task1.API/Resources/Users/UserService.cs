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
    public async Task<List<User>> GetUsers(UserFilter? filter)
    {
        return await _userCollection.Find(BuildFilter(filter)).ToListAsync();
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

    private FilterDefinition<User> BuildFilter(UserFilter? filter)
    { 
        var builder = Builders<User>.Filter;
        if (filter is null || !filter.HasFilter)
        {
            return builder.Empty;
        }

        List<FilterDefinition<User>> filters = new List<FilterDefinition<User>>();
        if (filter.HasNameFilter)
        {
            filters.Add(builder.Regex(e => e.Name, $"^{filter.NameStartWith}"));
        }

        if (filter.HasBornBeforeFilter)
        {
            filters.Add(builder.Gt(e => e.Dob, filter.BornBefore));
        }

        if (filter.HasBornAfterFilter)
        {
            filters.Add(builder.Lt(e => e.Dob, filter.BornAfter));
        }

        return builder.And(filters);
    }
}