using Library.Application.Services.Base;
using System.Security.Cryptography;

namespace Library.Application.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SALT_SIZE = 20;
    private const int ITERATIONS_COUNT = 10000;

    private static HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA256;

    public string HashPassword(string password)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(
            password: password,
            saltSize: SALT_SIZE,
            iterations: ITERATIONS_COUNT,
            hashAlgorithm: AlgorithmName);

        byte[] salt = deriveBytes.Salt;
        byte[] hash = deriveBytes.GetBytes(SALT_SIZE);

        byte[] hashBytes = new byte[SALT_SIZE * 2];
        Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
        Array.Copy(hash, 0, hashBytes, SALT_SIZE, SALT_SIZE);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[SALT_SIZE];
        Array.Copy(hashBytes, 0, salt, 0, SALT_SIZE);

        byte[] hash = new byte[SALT_SIZE];
        Array.Copy(hashBytes, SALT_SIZE, hash, 0, SALT_SIZE);

        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, ITERATIONS_COUNT, AlgorithmName);
        byte[] newHash = deriveBytes.GetBytes(SALT_SIZE);
        return AreByteArraysEqual(hash, newHash);
    }

    private bool AreByteArraysEqual(byte[] array1, byte[] array2) =>
        array1 != null &&
        array2 != null &&
        array1.Length == array2.Length &&
        array1.SequenceEqual(array2);
}