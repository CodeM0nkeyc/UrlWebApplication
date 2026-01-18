namespace UrlShortener.Application.Models;

public record Error(string Code, string Description = "")
{
    public static Error None = new Error(string.Empty);
    public static Error Internal = new Error("Internal", "Internal error occurred.");
    
    public static implicit operator Error(ValidationFailure? failure)
    {
        return new Error(failure?.PropertyName ?? "", failure?.ErrorMessage ?? "");
    }
}