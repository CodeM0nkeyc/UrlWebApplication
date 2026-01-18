namespace UrlShortener.Application.Features.ErrorCodeMapper;

public interface IErrorCodeMapper
{
    public string MapErrorCode(ErrorCode code);
}