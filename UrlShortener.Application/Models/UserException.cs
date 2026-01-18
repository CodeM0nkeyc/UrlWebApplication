namespace UrlShortener.Application.Models;

public class UserException : Exception
{
    public UserException(string message) : base(message)
    {
    }
}