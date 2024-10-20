public class EncryptionOptions
{ 
    public const string ConfigLocation = "Encryption";

    public required string PrivateKey { get; set; }
    public required string PublicCertificate { get; set; }
}