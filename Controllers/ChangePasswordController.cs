using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Helper;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;

public class ChangePasswordController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionUser _sessionUser;

    public ChangePasswordController(IUserRepository userRepository, ISessionUser sessionUser)
    {
        _userRepository = userRepository;
        _sessionUser = sessionUser;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // Retrieve user from session
        var user = _sessionUser.SearchUserSession();
        if (user == null)
        {
            TempData["ErrorMessage"] = "User session has expired. Please log in again.";
            return RedirectToAction("Index", "Login");
        }

        // Initialize the view model without email
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(ChangePasswordModel model)
    {


        var user = _sessionUser.SearchUserSession();
        if (user == null)
        {
            TempData["ErrorMessage"] = "User session has expired. Please log in again.";
            return RedirectToAction("Index", "Login");
        }

        if (model.NewPassword != model.ConfirmPassword)
        {
            TempData["ErrorMessage"] = "Passwords do not match.";
            return View(model);
        }

        var existingUser = _userRepository.SearchForEmail(user.Email);
        if (existingUser == null)
        {
            TempData["ErrorMessage"] = "User not found.";
            return View(model);
        }

        existingUser.Password = PasswordHasher.CreateHash(model.NewPassword);
        _userRepository.Update(existingUser);

        TempData["SuccessMessage"] = "Password changed successfully.";
        return RedirectToAction("Index", "Home");
    }
}
