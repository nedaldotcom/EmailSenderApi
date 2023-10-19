using System.Security.Cryptography;

namespace EmailSenderApi.Helpers;

public abstract class HashExtension
{
    public static string GenerateDefaultPassword()
    {
        using var cryptRng = RandomNumberGenerator.Create();
        var randomPasswordBuffer = new byte[6];
        cryptRng.GetBytes(randomPasswordBuffer);
        var generateDefaultPassword = Convert.ToBase64String(randomPasswordBuffer);
        return generateDefaultPassword;
    }
}