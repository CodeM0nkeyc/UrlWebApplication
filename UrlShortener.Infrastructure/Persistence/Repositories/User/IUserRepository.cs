namespace UrlShortener.Infrastructure.Persistence.Repositories.User;

public interface IUserRepository
{
    public Task<Entities.User?> GetIdAsync(int userId);
    public Task<Entities.User?> GetByEmailAsync(string email);
}