using Microsoft.AspNetCore.Builder;

namespace UrlShortener.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool showSqlSensitiveData)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts =>
        {
            opts.Cookie.Name = "Auth";
            opts.LoginPath = "/auth";
            opts.LogoutPath = "/auth/logout";
            opts.AccessDeniedPath = "/auth/denied";
            opts.ExpireTimeSpan = TimeSpan.FromDays(7);
        });

        services.AddAuthorizationCore(opts =>
        {
            opts.AddPolicy("OwnPolicy", policy =>
            {
                policy.AddRequirements(new NoUrlDataIdSpoofingRequirement());
            });
        });
        
        services.AddDbContext<UrlDbContext>(opts =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(configuration.GetConnectionString("UrlShortenerDb"));
            Console.ResetColor();
            
            opts.UseSqlServer(configuration.GetConnectionString("UrlShortenerDb")!,
                    optsBuilder => optsBuilder.EnableRetryOnFailure(3))
                .EnableSensitiveDataLogging(showSqlSensitiveData);
        });

        services.AddHttpContextAccessor();
        
        services.AddSingleton<IUrlChecker, UrlChecker>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<IUrlRepository, UrlRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAboutRepository, AboutRepository>();
        
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserDataAccessor, UserDataAccessor>();
        services.AddScoped<IAuthorizationHandler, NoUrlDataIdSpoofingHandler>();
        
        return services;
    }

    public static void MigrateDb(this IApplicationBuilder app)
    {
        using (IServiceScope scope = app.ApplicationServices.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
            db.Database.Migrate();
        }
    }
}