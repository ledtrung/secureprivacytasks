using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.EntityFrameworkCore.Extensions;

namespace SecurePrivacy.Task1.API.Resources.Users;

public class UsersDbContext : DbContext
{
    private readonly IOptionsMonitor<UserStoreOptions> _userStoreOptionsMonitor;

    public DbSet<User> Users { get; init; }

    public UsersDbContext(DbContextOptions options, IOptionsMonitor<UserStoreOptions> userStoreOptionsMonitor)
        : base(options)
    {
        _userStoreOptionsMonitor = userStoreOptionsMonitor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToCollection(_userStoreOptionsMonitor.CurrentValue.CollectionName);
    }
}