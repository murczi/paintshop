using Isopoh.Cryptography.Argon2;

public static class PasswordHasher
{
    public static string Hash(string password) => Argon2.Hash(password);
    public static bool Verify(string encodedHash, string password) => Argon2.Verify(encodedHash, password);
}
