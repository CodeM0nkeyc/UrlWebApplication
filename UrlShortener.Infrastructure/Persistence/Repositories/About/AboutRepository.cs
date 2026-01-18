namespace UrlShortener.Infrastructure.Persistence.Repositories.About;

public class AboutRepository : IAboutRepository
{
    private readonly UrlDbContext _urlDbContext;

    public AboutRepository(UrlDbContext urlDbContext)
    {
        _urlDbContext = urlDbContext;
    }
    
    public async Task<AboutSection?> GetByIdAsync(int id)
    {
        return await _urlDbContext.AboutSections.FindAsync(id);
    }

    public async Task<IEnumerable<AboutSection?>> GetManyAsync()
    {
        return await _urlDbContext.AboutSections.ToListAsync();
    }

    public async Task<AboutSection> AddAsync(AboutSection aboutSection)
    {
        AboutSection entity = _urlDbContext.AboutSections.Add(aboutSection).Entity;
        await _urlDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(AboutSection aboutSection)
    {
        _urlDbContext.AboutSections.Update(aboutSection);
        await _urlDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _urlDbContext.AboutSections.Remove(new AboutSection() { Id = id });
        await _urlDbContext.SaveChangesAsync();
    }
}