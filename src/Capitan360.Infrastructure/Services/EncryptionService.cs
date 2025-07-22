using System.Security.Cryptography;
using System.Text;
using Capitan360.Domain.Exceptions;

namespace Capitan360.Infrastructure.Services;

public class EncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public EncryptionService()
    {
        var rawKey = "ThisIsMy32CharSecretKey123456789"; // 32 کاراکتر
        var keyBytes = Encoding.UTF8.GetBytes(rawKey);
        _key = new byte[32]; // برای AES-256
        Array.Copy(keyBytes, 0, _key, 0, Math.Min(keyBytes.Length, _key.Length)); // تنظیم اندازه

        _iv = new byte[16];
        Array.Fill(_iv, (byte)0); // برای تست
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var input = Encoding.UTF8.GetBytes(plainText);
        var encrypted = encryptor.TransformFinalBlock(input, 0, input.Length);
        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string cipherText)
    {
        try
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var input = Convert.FromBase64String(cipherText);
            var decrypted = decryptor.TransformFinalBlock(input, 0, input.Length);
            return Encoding.UTF8.GetString(decrypted);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Decrypt error: " + ex.Message);
            throw new UnExpectedException("Error in Decryption");
        }
    }
}
