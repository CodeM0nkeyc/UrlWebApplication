namespace UrlShortener.Infrastructure.Persistence.Entities;

public class UserRole : EntityBase<int>
{
    public Role Role { get; set; }
}