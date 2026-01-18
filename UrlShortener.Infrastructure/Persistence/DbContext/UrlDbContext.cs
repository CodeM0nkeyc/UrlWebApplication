namespace UrlShortener.Infrastructure.Persistence.DbContext;

public class UrlDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public const int PageSize = 20;
    public DbSet<UrlData> Urls { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AboutSection> AboutSections { get; set; }

    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrlDbContext).Assembly);
        
        modelBuilder.Entity<UserRole>().HasData(new[]
        {
            new UserRole() { Id = 1, Role = Role.Admin },
            new UserRole() { Id = 2, Role = Role.User }
        });
        
        IPasswordHasher hasher = new PasswordHasher();

        User user1 = User.CreateUser("admin@email.com", "John", "Doe", "adminPass", 1, hasher);
        User user2 = User.CreateUser("user@email.com", "William", "Erlon", "userPass", 2, hasher);

        user1.Id = 1;
        user2.Id = 2;

        modelBuilder.Entity<User>().HasData(new[]
        {
            user1, user2
        });
    }
}