namespace UrlShortener.Infrastructure.Services.Security.Authorization.Handlers;

public class NoUrlDataIdSpoofingHandler : AuthorizationHandler<NoUrlDataIdSpoofingRequirement>
{
    private readonly UrlDbContext _urlDbContext;

    public NoUrlDataIdSpoofingHandler(UrlDbContext urlDbContext)
    {
        _urlDbContext = urlDbContext;
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        NoUrlDataIdSpoofingRequirement requirement)
    {
        int? id = (int?)context.Resource;
        
        if (id is null)
        {
            return;
        }

        int userId = int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        bool isOwner = await _urlDbContext.Urls.AnyAsync(x => x.Id == id && x.OwnerId == userId);

        if (context.User.HasClaim(ClaimTypes.Role, nameof(Role.Admin)) || isOwner)
        {
            context.Succeed(requirement);
            return;
        }
        
        context.Fail();
    }
}