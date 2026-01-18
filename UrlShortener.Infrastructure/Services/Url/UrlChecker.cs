using System.Net;

namespace UrlShortener.Infrastructure.Services.Url;

public class UrlChecker : IUrlChecker, IDisposable
{
    private bool _isDisposed = false;
    
    private readonly HttpClient _httpClient;
    
    public UrlChecker()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<UrlCheckData> CheckUrlAsync(string url)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        
        string contentType = response.Content.Headers.ContentType?.MediaType ?? "";

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(contentType);
        
        UrlCheckData urlCheckData = new UrlCheckData((int)response.StatusCode < 404, contentType);
        return urlCheckData;
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }
        
        _httpClient.Dispose();
        _isDisposed = true;
    }
}