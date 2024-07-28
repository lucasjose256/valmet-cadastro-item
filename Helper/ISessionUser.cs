using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Helper
{
    public interface ISessionUser
    {
        void CreateUserSession(UserModel usuario);

        void RemovoUserSession();

        UserModel SearchUserSession();
    }
}
