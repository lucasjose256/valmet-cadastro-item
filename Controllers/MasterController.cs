using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Filters;
using valmet_cadastro_item.smtp;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;
using valmet_cadastro_item.Helper;
using Microsoft.EntityFrameworkCore.Query;

namespace valmet_cadastro_item.Controllers
{
    [MasterPage]

    public class MasterController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender emailSender;
        private string sendKey;
        public MasterController(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

       
        public IActionResult Edit(int id)
        {

            try
            {
                UserModel model = _userRepository.SearchForId(id);
                if (ModelState.IsValid)
                {  
                    model = _userRepository.Update(model);
                    TempData["SuccessMessage"] = "User created successfully!";
                    return View(model);
                }
                //Criar uma tela de erro
                return RedirectToAction("Index", "Restrict");
            }
            catch (Exception ex)
            {
                //Criar uma tela de erro
                return RedirectToAction("Index","Restrict");

            }
        }
        [HttpPost]
        public IActionResult Update(UserModel model)
        {

             _userRepository.Update(model);
            return RedirectToAction("Index");

        }
    
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Table");
        }


        public IActionResult Table()
        {
            List<UserModel> users = _userRepository.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
            //Verificar o campo da senha q ele apaga.
        {
            sendKey = PasswordGenerate.generate();
            model.Password = sendKey;
            if (ModelState.IsValid)
            {
              
                if (_userRepository.SearchForEmail(model.Email) == null)
                {
                    model = _userRepository.Add(model);

                    TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
                    //await SendEmail(model.Email);
                    string email = model.Email;
                    await emailSender.SendEmailAsync(email, "Sua conta foi criada com sucesso e você já pode acessar a plataforma. Abaixo estão suas informações de login:", "\nUsuário: " + email + "\nSenha: " + sendKey);
                    return RedirectToAction("Index");

                }
                TempData["ErrorMessage"] = "Esse Email já está cadastrado!";
            }
            return View(model);
        }

        /*public async Task<IActionResult> Create(UserModel model)
        {
            return View();
        }*/

    }
}