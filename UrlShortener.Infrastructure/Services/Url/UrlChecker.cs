namespace UrlShortener.Infrastructure.Services.Url;

public class UrlChecker : IUrlChecker, IDisposable
{
    private bool _isDisposed = false;
    
    private readonly HttpClient _httpClient;
    
    public UrlChecker()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }
    
    public async Task<UrlCheckData> CheckUrlAsync(string url)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.SendAsync(request);
        }
        catch (HttpRequestException ex)
        {
            return new UrlCheckData(false, null);
        }
        
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