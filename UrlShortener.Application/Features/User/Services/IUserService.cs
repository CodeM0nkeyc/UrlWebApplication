namespace UrlShortener.Application.Features.User.Services;

public interface IUserService
{
    public Task<Result> AuthenticateUserAsync(string email, string password);
    public Task LogoutAsync();
}