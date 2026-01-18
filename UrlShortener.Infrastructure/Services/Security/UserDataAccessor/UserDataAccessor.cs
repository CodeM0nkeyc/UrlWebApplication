namespace UrlShortener.Infrastructure.Services.Security.UserDataAccessor;

public class UserDataAccessor : IUserDataAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserDataAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId => GetClaimValue<int>(ClaimTypes.NameIdentifier, int.Parse);

    public Role? UserRole => GetClaimValue<Role>(ClaimTypes.Role, Enum.Parse<Role>);

    public string? FirstName => GetClaimValue<string>(ClaimTypes.Name, null!);

    public string? LastName => GetClaimValue<string>(ClaimTypes.Surname, null!);

    private TValue? GetClaimValue<TValue>(string claimType, Func<string, TValue?> converter, bool rawValue = false)
    {
        Claim? claim = _httpContextAccessor.HttpContext?.User?.FindFirst(claimType);

        if (claim is null)
        {
            return default;
        }

        if (typeof(TValue) == typeof(string))
        {
            return (TValue)(object)claim.Value;
        }
        
        return converter.Invoke(claim.Value);
    }
}