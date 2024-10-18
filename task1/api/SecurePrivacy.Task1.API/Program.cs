using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePrivacy.Task1.API.Resources.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<UserStoreOptions>(builder.Configuration.GetSection(UserStoreOptions.ConfigLocation));
builder.Services.AddSingleton<IMongoCollection<User>>(sp =>
{
    var userStoreOptions = sp.GetRequiredService<IOptionsMonitor<UserStoreOptions>>().CurrentValue;
    var mongoClient = new MongoClient(userStoreOptions.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(userStoreOptions.DatabaseName);
    return mongoDatabase.GetCollection<User>(userStoreOptions.CollectionName);
});
builder.Services.AddSingleton<UserService>();
builder.Services.AddHostedService<UserStoreIndexesHostedService>();


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
