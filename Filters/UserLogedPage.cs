using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Filters
{
    public class UserLogedPage:ActionFilterAttribute
      
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string userSession = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller","Login" },
                    { "action","Index"}
                }
                    
                    
                    );
            }
            else
            {
                UserModel user=JsonConvert.DeserializeObject<UserModel>(userSession);
                if (user == null)
                {

                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller","Login" },
                    { "action","Index"}
                }


                  );
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
