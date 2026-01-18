namespace UrlShortener.Infrastructure.Persistence.Entities;

public class User : EntityBase<int>
{
    public string Email { get; set; }
    
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public UserRole UserRole { get; set; }
    public int UserRoleId { get; set; }
    
    public ICollection<UrlData> Urls { get; set; }
    
    public static User CreateUser(
        string email,
        string firstName,
        string lastName,
        string password,
        int roleId,
        IPasswordHasher passwordHasher)
    {
        User user = new User()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            UserRoleId = roleId
        };

        byte[] passwordSalt = passwordHasher.GenerateSalt();
        byte[] passwordHash = passwordHasher.ComputeHash(Encoding.ASCII.GetBytes(password), passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        return user;
    }
}