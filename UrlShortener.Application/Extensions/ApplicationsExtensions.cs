namespace UrlShortener.Application.Extensions;

public static class ApplicationsExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration,
        string shortUrlBase,
        bool showSensitiveData)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationsExtensions).Assembly);
        services.AddInfrastructure(configuration, showSensitiveData);
        
        services.AddScoped<IAboutSectionService, AboutSectionService>();
        services.AddScoped<IUrlService, UrlService>();
        services.AddScoped<IUserService, UserService>();
        
        services.Configure<GlobalData>(opts => opts.ShortUrlBase = shortUrlBase);
        
        return services;
    }
}