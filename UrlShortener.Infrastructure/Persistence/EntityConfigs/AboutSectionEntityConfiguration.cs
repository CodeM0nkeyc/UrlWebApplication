namespace UrlShortener.Infrastructure.Persistence.EntityConfigs;

public class AboutSectionEntityConfiguration : IEntityTypeConfiguration<AboutSection>
{
    public void Configure(EntityTypeBuilder<AboutSection> builder)
    {
        builder.ToTable("AboutSection");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Title)
            .HasMaxLength(128);

        builder.HasData(new AboutSection
        {
            Id = 1,
            Title = "About",
            Text = "This project was created using Angular 21, ASP.NET Core 6, EF Core 6 and MSSQL 2022." +
                   "URL Checker works based on base62 encoding. Every url entry has own unique id number, so " +
                   "base62 will return unique short url every time for every new and current long urls.\n\n" +
                   "Home page is done with Angular. You can navigate through urls that are present in the table." +
                   "If you are authenticated, you can create own url matches, also delete them if they are yours." +
                   "Admin can delete every entry in the table.\n\nLogin page is done with Razor View. Under the hood, " +
                   "when user logs in, cookie is used to authenticate user. About page is done with Razor View as well, " +
                   "and if you are admin, you can click on text to modify it and save."
        });
    }
}