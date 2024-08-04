using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using valmet_cadastro_item.Helper;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;

namespace valmet_cadastro_item.Controllers
{
    public class CadastroController : Controller
    {

        private readonly IItemRepository _IItemRepository;


        public CadastroController(IItemRepository IItemRepository)
        {
            _IItemRepository = IItemRepository;

        }
        public IActionResult Index()
        {
            return View(new ItemModel());
        }

        [HttpPost]
        public IActionResult Create(ItemModel model, IFormFile desenho)
        {
            if (desenho != null && desenho.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    desenho.CopyTo(ms);
                    model.Desenho = ms.ToArray();
                }
            }

            /*if (ModelState.IsValid)
            {
                // Save the model to the database
                // _context.ItemModels.Add(model);
                // _context.SaveChanges();

                return RedirectToAction("Index");
            }*/
            _IItemRepository.Add(model);
            return View("Index", model);
        }
    }
}
