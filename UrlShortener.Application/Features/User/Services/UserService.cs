namespace UrlShortener.Application.Features.User.Services;

public class UserService : IUserService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IErrorCodeMapper _codeMapper;

    public UserService(IAuthenticationService authenticationService, IErrorCodeMapper codeMapper)
    {
        _authenticationService = authenticationService;
        _codeMapper = codeMapper;
    }
    
    public async Task<Result> AuthenticateUserAsync(string email, string password)
    {
        AuthenticationResult authResult = await _authenticationService.AuthenticateByPasswordAsync(email, password);

        if (authResult == AuthenticationResult.UserNotFound)
        {
            return Result.Failure(new Error(_codeMapper.MapErrorCode(ErrorCode.UserNotFound), "User is not found."));
        }

        if (authResult == AuthenticationResult.PasswordMismatch)
        {
            return Result.Failure(new Error(_codeMapper.MapErrorCode(ErrorCode.PasswordMismatch), 
                "Password is not correct"));
        }
        
        return Result.Success();
    }

    public async Task LogoutAsync()
    {
        await _authenticationService.LogoutAsync();
    }
}