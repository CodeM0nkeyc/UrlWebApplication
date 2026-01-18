namespace UrlShortener.Infrastructure.Persistence.Repositories.Url;

public class UrlRepository : IUrlRepository
{
    private readonly UrlDbContext _urlDbContext;

    public UrlRepository(UrlDbContext urlDbContext)
    {
        _urlDbContext = urlDbContext;
        _urlDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public async Task<UrlData?> GetByIdAsync(int urlId)
    {
        return await _urlDbContext.Urls.Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == urlId);
    }

    public async Task<IEnumerable<UrlData?>> GetManyAsync(int pageIndex)
    {
        return await _urlDbContext.Urls.Skip((pageIndex - 1) * UrlDbContext.PageSize)
            .Take(UrlDbContext.PageSize)
            .ToListAsync();
    }

    public async Task<string?> GetLongUrlAsync(string shortUrl)
    {
        return await _urlDbContext.Urls.Where(x => x.ShortValue == shortUrl)
            .Select(x => x.LongValue)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsAsync(string url)
    {
        return await _urlDbContext.Urls.AnyAsync(x => x.LongValue == url);
    }

    public async Task<UrlData> AddAsync(UrlData url)
    {
        UrlData entity = _urlDbContext.Urls.Add(url).Entity;
        await _urlDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateShortValueAsync(UrlData urlData, string shortValue)
    {
        UrlData url = _urlDbContext.Attach(urlData).Entity;
        url.ShortValue = shortValue;
        await _urlDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int urlId)
    {
        _urlDbContext.Urls.Remove(new UrlData() { Id = urlId });
        await _urlDbContext.SaveChangesAsync();
    }
}