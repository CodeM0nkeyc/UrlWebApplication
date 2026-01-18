namespace UrlShortener.Infrastructure.Persistence.Entities.Common;

public abstract class EntityBase<TKey>
{
    public TKey Id { get; set; }
}