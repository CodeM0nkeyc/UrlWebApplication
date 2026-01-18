namespace UrlShortener.Application.Features.Url.Dto;

public record UrlBaseDto
{
    public int Id { get; init; }
    public string LongValue { get; init; } 
    public string? ShortValue { get; init; }
    public bool IsOwner { get; init; }

    public UrlBaseDto(int id, string longValue, string? shortValue, bool isOwner)
    {
        Id = id;
        LongValue = longValue;
        ShortValue = shortValue;
        IsOwner = isOwner;
    }
    
    public static UrlBaseDto FromUrlData(UrlData urlData, bool isOwner)
    {
        return new UrlBaseDto(urlData.Id, urlData.LongValue, urlData.ShortValue, isOwner);
    }
}