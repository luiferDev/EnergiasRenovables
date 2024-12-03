using System.Security.Cryptography;

namespace EnergiasRenovables.Services;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using var hmac = new HMACSHA256();
        var salt = hmac.Key; // Sal aleatoria.
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }
    
    public static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        using var hmac = new HMACSHA256(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(hash);
    }
}