using Microsoft.AspNetCore.Authentication;

namespace UrlShortener.Infrastructure.Services.Security.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AuthenticationResult> AuthenticateByPasswordAsync(string email, string password)
    {
        User? user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            return AuthenticationResult.UserNotFound;
        }

        byte[] storedPasswordHash = user.PasswordHash;
        byte[] storedPasswordSalt = user.PasswordSalt;
        
        byte[] passwordHash = _passwordHasher.ComputeHash(Encoding.ASCII.GetBytes(password), storedPasswordSalt);

        bool passwordsEqual = ComparePasswordBytes(passwordHash, storedPasswordHash);

        if (passwordsEqual)
        {
            await AssignCookiesAsync(user, DateTimeOffset.UtcNow.AddDays(7));
            return AuthenticationResult.Success;
        }

        return AuthenticationResult.PasswordMismatch;
    }

    public async Task LogoutAsync()
    {
        await RemoveCookiesAsync();
    }

    private bool ComparePasswordBytes(byte[] password1, byte[] password2)
    {
        return password1.Length == password2.Length 
               && password1.SequenceEqual(password2);
    }

    private ClaimsPrincipal BuildClaimsPrincipal(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserRole.Role.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private async Task AssignCookiesAsync(User user, DateTimeOffset expiration)
    {
        ClaimsPrincipal principal = BuildClaimsPrincipal(user);

        CookieOptions cookieOptions = new CookieOptions()
        {
            SameSite = SameSiteMode.Lax,
            Secure = true,
            Expires = expiration,
            IsEssential = true
        };
            
        AuthenticationProperties properties = new AuthenticationProperties()
        {
            IsPersistent = true,
            ExpiresUtc = expiration
        };

        await _httpContextAccessor.HttpContext.SignInAsync(principal, properties);
        _httpContextAccessor.HttpContext.Response.Cookies.Append("authenticated", "true", cookieOptions);
    }

    private async Task RemoveCookiesAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("authenticated");
    }
}