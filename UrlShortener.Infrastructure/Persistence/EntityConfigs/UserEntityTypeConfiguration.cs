namespace UrlShortener.Infrastructure.Persistence.EntityConfigs;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasMany(x => x.Urls)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId);

        builder.HasOne(x => x.UserRole)
            .WithMany()
            .HasForeignKey(x => x.UserRoleId);

        builder.Property(x => x.Email)
            .HasMaxLength(256);
        
        builder.Property(x => x.PasswordHash)
            .HasMaxLength(512);

        builder.Property(x => x.PasswordSalt)
            .HasMaxLength(64);
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(32);
        
        builder.Property(x => x.LastName)
            .HasMaxLength(32);
    }
}