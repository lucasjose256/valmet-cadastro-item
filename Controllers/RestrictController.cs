using Microsoft.AspNetCore.Mvc;
using valmet_cadastro_item.Filters;

namespace valmet_cadastro_item.Controllers
{
    [UserLogedPage]
    public class RestrictController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
