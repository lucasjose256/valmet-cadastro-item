using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Filters;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;

namespace valmet_cadastro_item.Controllers
{
    [MasterPage]

    public class MasterController : Controller
    {
        private readonly IUserRepository _userRepository;
        public MasterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        public IActionResult Create(UserModel model)
            //Verificar o campo da senha q ele apaga.
        {
            if (ModelState.IsValid)
            {
              
                if (_userRepository.SearchForEmail(model.Email) == null)
                {
                    model = _userRepository.Add(model);
                    TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");

                }
                TempData["ErrorMessage"] = "Esse Email já está cadastrado!";
            }
            return View(model);
        }
    }
}