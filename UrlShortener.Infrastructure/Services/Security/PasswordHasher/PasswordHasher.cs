namespace UrlShortener.Infrastructure.Services.Security.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    private const int _iterations = 600000;
    private const int _hashLength = 64;
    
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
    
    public byte[] ComputeHash(byte[] password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _hashLength);
    }

    public byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(_hashLength);
    }
}