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

builder.Services.Configure<EncryptionOptions>(builder.Configuration.GetSection(EncryptionOptions.ConfigLocation));

builder.Services.AddSingleton<UserService>();
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
