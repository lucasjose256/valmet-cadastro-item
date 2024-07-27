using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;

namespace valmet_cadastro_item.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    UserModel user = _userRepository.SearchForEmail(loginModel.Email);
                    if (user != null)
                    {
                        if (user.Password == loginModel.Password)
                        {
                            return RedirectToAction("Index", "Home");

                        }
                    }

                }
                return View("Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");

                //trater erro com temp data, q mostra na tela como um alert
            }

        }
    }
}
