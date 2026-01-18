namespace UrlShortener.Infrastructure.Persistence.Repositories.About;

public interface IAboutRepository
{
    public Task<AboutSection?> GetByIdAsync(int id);
    public Task<IEnumerable<AboutSection?>> GetManyAsync();
    
    public Task<AboutSection> AddAsync(AboutSection aboutSection);
    public Task UpdateAsync(AboutSection aboutSection);
    public Task DeleteAsync(int id);
}