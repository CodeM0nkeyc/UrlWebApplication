namespace UrlShortener.Infrastructure.Services.Url;

public interface IUrlChecker
{
    public Task<UrlCheckData> CheckUrlAsync(string url);
}