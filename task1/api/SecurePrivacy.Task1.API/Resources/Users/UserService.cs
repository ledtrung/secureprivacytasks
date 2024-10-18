using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SecurePrivacy.Task1.API.Resources.Users;

public class UserService
{
    private readonly UsersDbContext _dbContext;

    public UserService(UsersDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    /// <summary>
    /// Get all users that satisfy the filter
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    /// <summary>
    /// Get user information
    /// </summary>
    /// <param name="id">Identity of the user</param>
    /// <returns></returns>
    public async Task<User?> GetUser(string id)
    {
        return await _dbContext.Users.FindAsync(id);
    }
    
    /// <summary>
    /// Insert a new user. Will throw exception if trying to update an existing user here.
    /// </summary>
    /// <param name="user">User information</param>
    public async Task<User> AddUser(User user)
    {
        var val = await _dbContext.Users.AddAsync(user);
        return val.Entity;
    }
}