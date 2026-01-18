namespace UrlShortener.Infrastructure.Services.Url;

public record UrlCheckData(bool IsUrlAccessible, string? ContentType);