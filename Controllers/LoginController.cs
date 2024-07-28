using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Helper;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;

namespace valmet_cadastro_item.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionUser _session;
        public LoginController(IUserRepository userRepository, ISessionUser session)
        {
            _userRepository = userRepository;
            _session = session;
        }
        public IActionResult Index()
        {
            if (_session.SearchUserSession() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        public IActionResult LogOff() {
            _session.RemovoUserSession();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? user = _userRepository.SearchForEmail(loginModel.Email);

                    if (user == null)
                    {
                        TempData["ErrorMessage"] = "Usuário não encontrado.";
                        return View("Index");
                    }

                    if (user.Password != loginModel.Password)
                    {
                        TempData["ErrorMessage"] = "Senha incorreta.";
                        return View("Index");
                    }
                    _session.CreateUserSession(user);
                    return RedirectToAction("Index", "Home");
                }

                TempData["ErrorMessage"] = "Ocorreu algum erro com os dados fornecidos.";
                return View("Index", loginModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Ops! Ocorreu algum erro de exceção.";
                return View("Index");
            }
        }

    }
}
