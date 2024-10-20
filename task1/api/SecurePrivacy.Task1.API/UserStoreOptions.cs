public class UserStoreOptions
{ 
    public const string ConfigLocation = "UserDatastore";

    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string UserCollection { get; set; }
    public required string ConsentCollection { get; set; }
}