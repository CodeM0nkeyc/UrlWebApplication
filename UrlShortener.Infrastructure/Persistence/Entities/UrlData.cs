namespace UrlShortener.Infrastructure.Persistence.Entities;

public class UrlData : EntityBase<int>
{
    public string LongValue { get; set; }
    public string? ShortValue { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string ResourceType { get; set; }
    
    public User Owner { get; set; }
    public int OwnerId { get; set; }
}