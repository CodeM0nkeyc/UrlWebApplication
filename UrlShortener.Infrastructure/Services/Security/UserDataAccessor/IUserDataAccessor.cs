namespace UrlShortener.Infrastructure.Services.Security.UserDataAccessor;

public interface IUserDataAccessor
{
    public int? UserId { get; }
    public Role? UserRole { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
}