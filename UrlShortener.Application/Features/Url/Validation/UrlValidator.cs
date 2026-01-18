namespace UrlShortener.Application.Features.Url.Validation;

public class UrlValidator : AbstractValidator<UrlBaseDto>
{
    public UrlValidator(IErrorCodeMapper codeMapper, IUrlRepository urlRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.LongValue)
            .NotNull().WithCodeAndMessage(codeMapper.MapErrorCode(ErrorCode.UrlNull), "URL is required.")
            
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithCodeAndMessage(codeMapper.MapErrorCode(ErrorCode.InvalidUrl), "Url is not valid as absolute.")
            
            .MustAsync(async (value, token) => !await urlRepository.ExistsAsync(value))
            .WithCodeAndMessage(codeMapper.MapErrorCode(ErrorCode.UrlAlreadyExists), "Url already exists");
    }
}