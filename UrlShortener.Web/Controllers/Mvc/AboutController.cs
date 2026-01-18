namespace UrlShortener.Web.Controllers.Mvc;

public class AboutController : Controller
{
    private readonly IAboutSectionService _aboutSectionService;

    public AboutController(IAboutSectionService aboutSectionService)
    {
        _aboutSectionService = aboutSectionService;
    }
    
    public async Task<IActionResult> Index()
    {
        Result<AboutSectionDto?> result = await _aboutSectionService.GetAboutSectionByIdAsync(1);
        AboutVm aboutVm = result.Data!;
        
        return View(aboutVm);
    }

    [Authorize(Roles = nameof(Role.Admin))]
    [HttpPost]
    public async Task<IActionResult> UpdateAboutSection(AboutVm aboutVm)
    {
        Result result = await _aboutSectionService.UpdateSectionAsync(aboutVm);

        if (!result.IsSuccess)
        {
            foreach (Error error in result.Errors!)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View("Index");
        }
        
        return RedirectToAction("Index");
    }
}