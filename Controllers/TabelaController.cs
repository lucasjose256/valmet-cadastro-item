using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Models;


namespace valmet_cadastro_item.Controllers
{
    public class TabelaController : Controller
    {
        private readonly IItemRepository _itemRepositorio;

        public TabelaController(IItemRepository itemRepositorio)
        {
            _itemRepositorio = itemRepositorio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ItemModel> items = _itemRepositorio.BuscarTodos();
            return View(items);
        }
    }
}
