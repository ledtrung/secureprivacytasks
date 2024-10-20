namespace SecurePrivacy.Task1.API.Resources.Users;

public class UserDTO
{
    public UserDTO(string? id, string name, string email, string address, DateTime dob)
    {
        Id = id;
        Name = name;
        Email = email;
        Address = address;
        Dob = dob;
    }

    public string? Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public DateTime Dob { get; private set; }

    public static UserDTO FromUser(User user, CryptographyService cryptoService) 
        => new UserDTO(
            id: user.Id, 
            name: user.Name, 
            email: string.IsNullOrWhiteSpace(user.Email) ? user.Email : cryptoService.Decrypt(user.Email), 
            address: user.Address, 
            dob: user.Dob);
    public User ToUser(CryptographyService cryptoService) 
        => new User(
            id: Id, 
            name: Name, 
            email: string.IsNullOrWhiteSpace(Email) ? Email : cryptoService.Encrypt(Email), 
            address: Address, 
            dob: Dob);
}