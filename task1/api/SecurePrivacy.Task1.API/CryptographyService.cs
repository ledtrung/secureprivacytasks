using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Options;

public class CryptographyService
{
    private readonly IOptionsMonitor<EncryptionOptions> _encryptionOptionsMonitor;

    private readonly RSAEncryptionPadding PADDING = RSAEncryptionPadding.OaepSHA1;

    public CryptographyService(IOptionsMonitor<EncryptionOptions> encryptionOptionsMonitor)
    {
        _encryptionOptionsMonitor = encryptionOptionsMonitor;
    }

    public string Encrypt(string input)
    {
        var currentOptions = _encryptionOptionsMonitor.CurrentValue;
        var pemBytes = Convert.FromBase64String(currentOptions.PublicCertificate);
        var cert = new X509Certificate2(pemBytes);
        var rsa = cert.GetRSAPublicKey()!;

        return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(input), PADDING));
    }

    public string Decrypt(string encryptedInput)
    {
        using var rsa = LoadPrivateKey();
        var decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedInput), PADDING);
        return Encoding.UTF8.GetString(decryptedData);
    }

    private RSA LoadPrivateKey()
    { 
        var currentOptions = _encryptionOptionsMonitor.CurrentValue;

        var rsa = RSA.Create();
        var keyBytes = Convert.FromBase64String(currentOptions.PrivateKey);
        rsa.ImportPkcs8PrivateKey(keyBytes, out _);

        return rsa;
    }
}