namespace SecurePrivacy.Task1.API.Resources.Consent;

public class ConsentType
{
    public ConsentType(string type, bool mandatory)
    {
        Type = type;
        Mandatory = mandatory;
    }

    public string Type { get; private set; }
    public bool Mandatory { get; private set; }
}