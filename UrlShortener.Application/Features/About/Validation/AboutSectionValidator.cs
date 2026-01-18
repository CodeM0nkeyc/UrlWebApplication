namespace UrlShortener.Application.Features.About.Validation;

public class AboutSectionValidator : AbstractValidator<AboutSectionDto>
{
    public AboutSectionValidator(IErrorCodeMapper codeMapper)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Title)
            .NotNull().WithCodeAndMessage(codeMapper.MapErrorCode(ErrorCode.TitleRequired), "Title is required.")
            .MaximumLength(128)
            .WithCodeAndMessage(codeMapper.MapErrorCode(ErrorCode.TitleTooLong),
                "The text must not exceed 128 characters.");

        RuleFor(x => x.Text)
            .NotNull().WithCodeAndMessage(codeMapper.MapErrorCode(ErrorCode.TextRequired), "Text is required.");
    }
}