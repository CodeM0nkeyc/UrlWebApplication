namespace UrlShortener.Application.Features.Url.Services;

public interface IUrlService
{
    public Task<Result<UrlDetailedDto?>> GetUrlByIdAsync(int urlId);
    public Task<Result<IEnumerable<UrlBaseDto>?>> GetUrlsAsync(int pageIndex);
    public Task<Result<string?>> GetLongUrlAsync(string shortUrl);
    
    public Task<Result<UrlDetailedDto?>> CreateUrlMatchAsync(string url);
    public Task<Result> DeleteUrlAsync(int id);
}