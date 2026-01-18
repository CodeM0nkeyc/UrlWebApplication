namespace UrlShortener.Application.Features.Url.Dto;

public record UrlDetailedDto : UrlBaseDto
{
    public Creator CreatedBy { get; init; }
    public DateTime CreatedDate { get; init; }

    public string ResourceType { get; init; }

    public UrlDetailedDto(
        
        int id,
        string longValue,
        string? shortValue,
        Creator createdBy,
        DateTime createdDate,
        string resourceType,
        bool isOwner
        
    ) : base(id, longValue, shortValue, isOwner)
    {
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        ResourceType = resourceType;
    }

    public new static UrlDetailedDto FromUrlData(UrlData urlData, bool isOwner)
    {
        Creator createdBy = new Creator()
        {
            FirstName = urlData.Owner?.FirstName ?? "",
            LastName = urlData.Owner?.LastName ?? ""
        };
        
        return new UrlDetailedDto(urlData.Id, urlData.LongValue, urlData.ShortValue, 
            createdBy, urlData.CreatedAt, urlData.ResourceType, isOwner);
    }
    
    public static UrlDetailedDto FromUrlData(UrlData urlData, Creator creator, bool isOwner)
    {
        return new UrlDetailedDto(urlData.Id, urlData.LongValue, urlData.ShortValue, 
            creator, urlData.CreatedAt, urlData.ResourceType, isOwner);
    }
}