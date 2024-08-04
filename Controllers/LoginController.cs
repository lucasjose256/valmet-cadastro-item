using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Helper;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;
using valmet_cadastro_item.smtp;

namespace valmet_cadastro_item.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionUser _session;
        private readonly IEmailSender emailSender;
        private string sendKey;

        public LoginController(IUserRepository userRepository, ISessionUser session, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
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
        [HttpPost]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            await emailSender.SendEmailAsync(email, subject, message);
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

                    if (user.Password != loginModel.Password.CreateHash())
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

        public IActionResult Retrieve()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Retrieve(LoginModel loginModel)
        {
            var user = _userRepository.SearchForEmail(loginModel.Email);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Usuário não foi encontrado.";
                return View("Retrieve");
            }
            var sendKey = PasswordGenerate.generate();
            user.Password = PasswordHasher.CreateHash(sendKey);
            _userRepository.Update(user);
            await emailSender.SendEmailAsync(loginModel.Email, "Sua nova senha foi gerada com sucesso, detalhes de login abaixo:", "\nUser: " + loginModel.Email + "\nPassword: " + sendKey);

            TempData["SuccessMessage"] = "Uma nova senha de acesso foi enviado para seu email.";
            return View("Retrieve");
        }

    }
}
