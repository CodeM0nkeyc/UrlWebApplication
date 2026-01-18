namespace UrlShortener.Application.Features.About.Dto;

public record AboutSectionDto(int Id, string Title, string Text)
{
    public static implicit operator AboutSectionDto(AboutSection section)
    {
        return new AboutSectionDto(section.Id, section.Title, section.Text);
    }
    
    public static implicit operator AboutSection(AboutSectionDto sectionDto)
    {
        return new AboutSection()
        {
            Id = sectionDto.Id,
            Title = sectionDto.Title,
            Text = sectionDto.Text
        };
    }
}