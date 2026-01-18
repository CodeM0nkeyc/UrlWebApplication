namespace UrlShortener.Web.Services;

public class ErrorCodeMapper : IErrorCodeMapper
{
    private readonly Dictionary<ErrorCode, string> _codePropertyMaps = new Dictionary<ErrorCode, string>()
    {
        [ErrorCode.UrlNull] = "Url",
        [ErrorCode.InvalidUrl] = "Url",
        [ErrorCode.UrlNotAccessible] = "Url",
        [ErrorCode.UrlAlreadyExists] = "Url",
        [ErrorCode.UrlNotFound] = "Url",
        
        [ErrorCode.UserNotFound] = $"{nameof(LoginVm.Email)}",
        [ErrorCode.PasswordMismatch] =  $"{nameof(LoginVm.Password)}",
        
        [ErrorCode.TitleRequired] = $"{nameof(AboutVm.Title)}",
        [ErrorCode.TitleTooLong] = $"{nameof(AboutVm.Title)}",
        [ErrorCode.TextRequired] = $"{nameof(AboutVm.Text)}"
    };
    
    public string MapErrorCode(ErrorCode code)
    {
        return _codePropertyMaps.GetValueOrDefault(code) ?? "";
    }
}