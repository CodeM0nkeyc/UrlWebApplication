namespace UrlShortener.Web.Controllers.Mvc;

public class HomeController : Controller
{
    private readonly IUrlService _urlService;

    public HomeController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    public IActionResult Index()
    {
        return View();
    }
}