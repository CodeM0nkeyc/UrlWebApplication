namespace UrlShortener.Infrastructure.Persistence.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly UrlDbContext _urlDbContext;

    public UserRepository(UrlDbContext urlDbContext)
    {
        _urlDbContext = urlDbContext;
        _urlDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public async Task<Entities.User?> GetIdAsync(int userId)
    {
        return await _urlDbContext.Users.Include(x => x.UserRole)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<Entities.User?> GetByEmailAsync(string email)
    {
        return await _urlDbContext.Users.Include(x => x.UserRole)
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}