namespace UrlShortener.Infrastructure.Services.Security.Authentication;

public interface IAuthenticationService
{
    public Task<AuthenticationResult> AuthenticateByPasswordAsync(string email, string password);
    public Task LogoutAsync();
}