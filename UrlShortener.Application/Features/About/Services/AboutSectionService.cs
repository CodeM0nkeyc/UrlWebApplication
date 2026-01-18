namespace UrlShortener.Application.Features.About.Services;

public class AboutSectionService : IAboutSectionService
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IValidator<AboutSectionDto> _validator;

    public AboutSectionService(IAboutRepository aboutRepository, IValidator<AboutSectionDto> validator)
    {
        _aboutRepository = aboutRepository;
        _validator = validator;
    }

    public async Task<Result<AboutSectionDto?>> GetAboutSectionByIdAsync(int sectionId)
    {
        AboutSectionDto? aboutSectionDto = (await _aboutRepository.GetByIdAsync(sectionId))!;
        return Result<AboutSectionDto?>.Success(aboutSectionDto);
    }

    public async Task<Result> AddSectionAsync(AboutSectionDto aboutSectionDto)
    {
        ValidationResult vResult = _validator.Validate(aboutSectionDto);

        if (!vResult.IsValid)
        {
            return Result.Failure(vResult.Errors.FirstOrDefault());
        }

        AboutSection aboutSection = new AboutSection()
        {
            Text = aboutSectionDto.Text,
            Title = aboutSectionDto.Title
        };

        await _aboutRepository.AddAsync(aboutSection);
        return Result.Success();
    }

    public async Task<Result> UpdateSectionAsync(AboutSectionDto aboutSectionDto)
    {
        await _aboutRepository.UpdateAsync(aboutSectionDto);
        return Result.Success();
    }

    public async Task<Result> RemoveSectionAsync(int sectionId)
    {
        await _aboutRepository.DeleteAsync(sectionId);
        return Result.Success();
    }
}