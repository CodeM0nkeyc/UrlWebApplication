namespace UrlShortener.Web.Models;

public record AboutVm(string Title, string Text)
{
    public static implicit operator AboutVm(AboutSectionDto aboutSectionDto)
    {
        return new AboutVm(aboutSectionDto.Title, aboutSectionDto.Text);
    }
    
    public static implicit operator AboutSectionDto(AboutVm aboutVm)
    {
        return new AboutSectionDto(1, aboutVm.Title, aboutVm.Text);
    }
}