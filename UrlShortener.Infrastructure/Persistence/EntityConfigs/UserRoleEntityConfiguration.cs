namespace UrlShortener.Infrastructure.Persistence.EntityConfigs;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Role)
            .HasMaxLength(20)
            .HasConversion<EnumToStringConverter<Role>>();
    }
}