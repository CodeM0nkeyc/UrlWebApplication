namespace UrlShortener.Infrastructure.Persistence.Repositories.Url;

public interface IUrlRepository
{
    public Task<UrlData?> GetByIdAsync(int urlId);
    public Task<IEnumerable<UrlData?>> GetManyAsync(int pageIndex);
    
    public Task<string?> GetLongUrlAsync(string shortUrl);
    
    public Task<bool> ExistsAsync(string url);
    
    public Task<UrlData> AddAsync(UrlData url);
    public Task UpdateShortValueAsync(UrlData urlData, string shortValue);
    public Task DeleteAsync(int urlId);
}