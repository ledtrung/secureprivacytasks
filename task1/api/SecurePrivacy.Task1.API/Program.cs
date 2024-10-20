using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePrivacy.Task1.API.Resources.Consent;
using SecurePrivacy.Task1.API.Resources.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<UserStoreOptions>(builder.Configuration.GetSection(UserStoreOptions.ConfigLocation));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var userStoreOptions = sp.GetRequiredService<IOptionsMonitor<UserStoreOptions>>().CurrentValue;
    var mongoClient = new MongoClient(userStoreOptions.ConnectionString);
    return mongoClient.GetDatabase(userStoreOptions.DatabaseName);
});

builder.Services.AddSingleton<IMongoCollection<User>>(sp =>
{
    var userStoreOptions = sp.GetRequiredService<IOptionsMonitor<UserStoreOptions>>().CurrentValue;
    var database = sp.GetRequiredService<IMongoDatabase>();
    return database.GetCollection<User>(userStoreOptions.UserCollection);
});

builder.Services.AddSingleton<IMongoCollection<Consent>>(sp =>
{
    var userStoreOptions = sp.GetRequiredService<IOptionsMonitor<UserStoreOptions>>().CurrentValue;
    var database = sp.GetRequiredService<IMongoDatabase>();
    return database.GetCollection<Consent>(userStoreOptions.ConsentCollection);
});

builder.Services.Configure<EncryptionOptions>(builder.Configuration.GetSection(EncryptionOptions.ConfigLocation));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ConsentService>();
builder.Services.AddSingleton<CryptographyService>();
builder.Services.AddHostedService<UserStoreIndexesHostedService>();

builder.Services.AddCors(options =>
    {
        //This should be configurable
        options.AddDefaultPolicy(builder => {
            builder.WithOrigins("http://localhost:4200");
            builder.WithMethods("GET", "POST");
            builder.AllowAnyHeader();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
