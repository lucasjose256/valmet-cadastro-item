using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.ViewComponents
{
    public class Menu:ViewComponent    {

        public async Task<IViewComponentResult> InvokeAsync()
        {//precisa tratar essa exeção

            string userSession = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(userSession)) return null;
            UserModel user= JsonConvert.DeserializeObject<UserModel>(userSession);
            return View(user);
        }
    }
}
