using Newtonsoft.Json;
using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Helper
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public Session(HttpContextAccessor contextAccessor)
        {
            _contextAccessor=contextAccessor;
        }
        public void CreateUserSession(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);
            _contextAccessor.HttpContext.Session.SetString("sessaoUsuarioLogado", value);
        }

        public void RemovoUserSession()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }

        public UserModel SearchUserSession()
        {
            string userSession = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(userSession)) return null;

            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }
    }
}
