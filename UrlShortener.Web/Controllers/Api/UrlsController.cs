namespace UrlShortener.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class UrlsController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlsController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UrlBaseDto>>> GetUrls([FromQuery] int pageIndex)
    {
        if (pageIndex < 1)
        {
            return BadRequest(Enumerable.Repeat(new Error("InvalidPageInex", "Page index must be greater that 0"), 1));
        }
        
        Result<IEnumerable<UrlBaseDto>?> result = await _urlService.GetUrlsAsync(pageIndex);
        return Ok(result.Data);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UrlDetailedDto>> GetUrl(int id)
    {
        Result<UrlDetailedDto?> result = await _urlService.GetUrlByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        
        return Ok(result.Data);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<UrlDetailedDto?>> CreateUrlMatching([FromBody] string url)
    {
        Result<UrlDetailedDto?> result = await _urlService.CreateUrlMatchAsync(url);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors );
        }

        return Ok(result.Data);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUrlAsync(int id, [FromServices] IAuthorizationService authorizationService)
    {
        var authorizationResult = 
            await authorizationService.AuthorizeAsync(User, id, new NoUrlDataIdSpoofingRequirement());

        if (!authorizationResult.Succeeded)
        {
            return Forbid(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
        Result result = await _urlService.DeleteUrlAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}