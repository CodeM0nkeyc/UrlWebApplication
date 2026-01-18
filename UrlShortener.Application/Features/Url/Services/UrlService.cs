namespace UrlShortener.Application.Features.Url.Services;

public class UrlService : IUrlService
{
    private readonly string _baseUrl;
    
    private readonly IUrlRepository _urlRepository;
    private readonly IValidator<UrlBaseDto> _validator;
    private readonly IUrlChecker _urlChecker;
    private readonly IUserDataAccessor _userDataAccessor;
    private readonly IErrorCodeMapper _codeMapper;

    public UrlService(
        IUrlRepository urlRepository,
        IValidator<UrlBaseDto> validator,
        IUrlChecker urlChecker,
        IUserDataAccessor userDataAccessor,
        IErrorCodeMapper codeMapper,
        IOptions<GlobalData> globalData)
    {
        _baseUrl = globalData.Value.ShortUrlBase;
        
        _urlRepository = urlRepository;
        _validator = validator;
        _urlChecker = urlChecker;
        _userDataAccessor = userDataAccessor;
        _codeMapper = codeMapper;
    }

    public async Task<Result<UrlDetailedDto?>> GetUrlByIdAsync(int urlId)
    {
        UrlData? url = await _urlRepository.GetByIdAsync(urlId);

        if (url is null)
        {
            return Result<UrlDetailedDto?>.Failure(new Error("NoUrlFound", "No url was found"));
        }

        return Result<UrlDetailedDto>.Success(UrlDetailedDto.FromUrlData(url, IsOwner(url)));
    }

    public async Task<Result<IEnumerable<UrlBaseDto>?>> GetUrlsAsync(int pageIndex)
    {
        IEnumerable<UrlData?> urls = await _urlRepository.GetManyAsync(pageIndex);
        return Result<IEnumerable<UrlBaseDto>>.Success(
            urls.Select(x => UrlBaseDto.FromUrlData(x!, IsOwner(x!))));
    }

    public async Task<Result<string?>> GetLongUrlAsync(string shortUrl)
    {
        string? longUrl = await _urlRepository.GetLongUrlAsync(shortUrl);

        if (longUrl is null)
        {
            Error error = new Error(_codeMapper.MapErrorCode(ErrorCode.UrlNotFound), $"No match for url {shortUrl}");
            return Result<string?>.Failure(error);
        }

        return Result<string?>.Success(longUrl);
    }

    public async Task<Result<UrlDetailedDto?>> CreateUrlMatchAsync(string url)
    {
        UrlData urlData = new UrlData()
        {
            LongValue = url,
            CreatedAt = DateTime.UtcNow
        };

        ValidationResult vResult = await _validator.ValidateAsync(new UrlBaseDto(-1, url, "", false));

        if (!vResult.IsValid)
        {
            return Result<UrlDetailedDto?>.Failure(vResult.Errors.FirstOrDefault());
        }

        UrlCheckData urlCheckData = await _urlChecker.CheckUrlAsync(url);

        if (!urlCheckData.IsUrlAccessible)
        {
            Error vError = new Error(_codeMapper.MapErrorCode(ErrorCode.UrlNotAccessible), "Url is not accessible.");
            return Result<UrlDetailedDto>.Failure(vError);
        }
        
        urlData.OwnerId = _userDataAccessor.UserId!.Value;
        urlData.ResourceType = urlCheckData.ContentType ?? "";
        
        UrlData addedData = await _urlRepository.AddAsync(urlData);
        await _urlRepository.UpdateShortValueAsync(addedData, ShortenUrl(addedData.Id));

        Creator creator = new Creator()
        {
            FirstName = _userDataAccessor.FirstName ?? "",
            LastName = _userDataAccessor.LastName ?? "",
        };
        
        return Result<UrlDetailedDto>.Success(UrlDetailedDto.FromUrlData(addedData, creator, true));
    }

    public async Task<Result> DeleteUrlAsync(int id)
    {
        await _urlRepository.DeleteAsync(id);
        return Result.Success();
    }

    private string ShortenUrl(int urlId)
    {
        return _baseUrl + urlId.ToBase62();
    }

    private bool IsOwner(UrlData url)
    {
        return _userDataAccessor.UserRole == Role.Admin
               || _userDataAccessor.UserId == url.OwnerId;
    }
}