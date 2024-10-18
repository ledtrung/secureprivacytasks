public class UserStoreOptions
{ 
    public const string ConfigLocation = "UserDatastore";

    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionName { get; set; }
}