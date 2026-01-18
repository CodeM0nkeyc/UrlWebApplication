namespace UrlShortener.Infrastructure.Persistence.Entities;

public class AboutSection : EntityBase<int>
{
    public string Title { get; set; }
    public string Text { get; set; }
}