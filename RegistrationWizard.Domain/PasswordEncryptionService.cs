using System.Security.Cryptography;
using System.Text;

namespace RegistrationWizard.Domain;

public class PasswordEncryptionService
{
    private const string EncryptionKey = "8f09a0ea-0c34-4efb-af10-3250405ffc45";
    private static readonly byte[] Salt = "8f09a0ea-0c34-4efb-af10-3250405ffc45"u8.ToArray();

    public static string Encrypt(string clearText)
    {
        var clearBytes = Encoding.Unicode.GetBytes(clearText);

        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(EncryptionKey, Salt, 2000, HashAlgorithmName.SHA1);

        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
        }

        clearText = Convert.ToBase64String(ms.ToArray());

        return clearText;
    }

    public static string Decrypt(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        var cipherBytes = Convert.FromBase64String(cipherText);

        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(EncryptionKey, Salt, 2000, HashAlgorithmName.SHA1);

        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
        }
        cipherText = Encoding.Unicode.GetString(ms.ToArray());

        return cipherText;
    }
}