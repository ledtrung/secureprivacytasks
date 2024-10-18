using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePrivacy.Task1.API.Resources.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<UserStoreOptions>(builder.Configuration.GetSection(UserStoreOptions.ConfigLocation));
builder.Services.AddSingleton(sp =>
{
    var userStoreOptionsMonitor = sp.GetRequiredService<IOptionsMonitor<UserStoreOptions>>();
    var mongoClient = new MongoClient(userStoreOptionsMonitor.CurrentValue.ConnectionString);
    var dbContextOptions 
        = new DbContextOptionsBuilder<UsersDbContext>()
            .UseMongoDB(mongoClient, userStoreOptionsMonitor.CurrentValue.DatabaseName);

    return new UsersDbContext(dbContextOptions.Options, userStoreOptionsMonitor);
});
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
