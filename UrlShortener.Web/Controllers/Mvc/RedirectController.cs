namespace UrlShortener.Web.Controllers.Mvc;

[Route("r")]
public class RedirectController : Controller
{
    private readonly IUrlService _urlService;

    public RedirectController(IUrlService urlService)
    {
        _urlService = urlService;
    }
    
    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> Index(string shortUrl)
    {
        Result<string?> result = await _urlService.GetLongUrlAsync(HttpContext.Request.Path);

        if (!result.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        return Redirect(result.Data ?? "/");
    }
}