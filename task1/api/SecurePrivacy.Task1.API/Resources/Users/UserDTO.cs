namespace SecurePrivacy.Task1.API.Resources.Users;

public class UserDTO
{
    public UserDTO(string? id, string name, string address, DateTime dob)
    {
        Id = id;
        Name = name;
        Address = address;
        Dob = dob;
    }

    public string? Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public DateTime Dob { get; private set; }

    public static UserDTO FromUser(User user) => new UserDTO(user.Id, user.Name, user.Address, user.Dob);
    public User ToUser() => new User(Id, Name, Address, Dob);
}