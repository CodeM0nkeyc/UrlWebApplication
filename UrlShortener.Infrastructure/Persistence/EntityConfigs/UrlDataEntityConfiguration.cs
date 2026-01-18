namespace UrlShortener.Infrastructure.Persistence.EntityConfigs;

public class UrlDataEntityConfiguration : IEntityTypeConfiguration<UrlData>
{
    public void Configure(EntityTypeBuilder<UrlData> builder)
    {
        builder.ToTable("UrlsData");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(nameof(UrlData.LongValue), nameof(UrlData.ShortValue))
            .IsUnique();

        builder.HasIndex(x => x.OwnerId);

        builder.Property(x => x.LongValue)
            .HasMaxLength(2048);
        
        builder.Property(x => x.ShortValue)
            .HasMaxLength(64);
        
        builder.Property(x => x.ResourceType)
            .HasMaxLength(64);
    }
}