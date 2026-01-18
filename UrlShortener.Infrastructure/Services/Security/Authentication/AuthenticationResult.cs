namespace UrlShortener.Infrastructure.Services.Security.Authentication;

public enum AuthenticationResult
{
    Success,
    UserNotFound,
    PasswordMismatch
}