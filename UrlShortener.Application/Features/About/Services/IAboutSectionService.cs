namespace UrlShortener.Application.Features.About.Services;

public interface IAboutSectionService
{
    public Task<Result<AboutSectionDto?>> GetAboutSectionByIdAsync(int sectionId);
    public Task<Result> AddSectionAsync(AboutSectionDto aboutSectionDto);
    public Task<Result> UpdateSectionAsync(AboutSectionDto aboutSectionDto);
    public Task<Result> RemoveSectionAsync(int sectionId);
}