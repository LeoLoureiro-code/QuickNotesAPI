using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public class PasswordService
{
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        byte[] hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            32);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string hashString)
    {
        var parts = hashString.Split('.');
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        byte[] hashToCompare = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            32);

        return hash.SequenceEqual(hashToCompare);
    }
}
