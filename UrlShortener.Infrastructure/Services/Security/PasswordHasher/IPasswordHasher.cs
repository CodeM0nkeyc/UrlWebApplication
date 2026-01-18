namespace UrlShortener.Infrastructure.Services.Security.PasswordHasher;

public interface IPasswordHasher
{
    public byte[] ComputeHash(byte[] password, byte[] salt);
    public byte[] GenerateSalt();
}