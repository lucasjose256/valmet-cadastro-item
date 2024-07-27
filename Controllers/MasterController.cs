using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;

namespace valmet_cadastro_item.Controllers
{
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
                model = _userRepository.Add(model);
                TempData["SuccessMessage"] = "User created successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}