namespace UrlShortener.Web.Controllers.Mvc;

public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    
    public IActionResult Index()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index","Home");
        }
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm loginVm, string? returnUrl)
    {
        Result result = await _userService.AuthenticateUserAsync(loginVm.Email, loginVm.Password);

        if (result.IsSuccess)
        {
            if (returnUrl is null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return LocalRedirect(returnUrl);
        }

        foreach (Error error in result.Errors!)
        {
            ModelState.AddModelError(error.Code, error.Description!);
        }
        
        return View("Index");
    }
    
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}