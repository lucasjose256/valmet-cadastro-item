using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Enums;

namespace valmet_cadastro_item.Filters
{
    public class MasterPage : ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string userSession = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {   {"controller","Login" },
                    { "action","Index"}});

            }
            else
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(userSession);
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {   {"controller","Login" },
                    { "action","Index"}});


                }
                else if (user.Perfil != ProfileEnum.Mestre)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {   {"controller","Restrict" },
                    { "action","Index"}});

                }
            }

            base.OnActionExecuting(context);
        }
    }
}
