namespace UrlShortener.Application.Models;

public enum ErrorCode
{
    // Generic
    None,
    Internal,
    
    // Urls
    UrlNull,
    InvalidUrl,
    UrlNotAccessible,
    UrlAlreadyExists,
    UrlNotFound,
    
    // User
    UserNotFound,
    PasswordMismatch,
    
    // AboutSection
    TitleRequired,
    TitleTooLong,
    TextRequired
}