using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Repositories
{
    public interface IUserRepository
    {
        UserModel SearchForEmail(string email);
        List<UserModel> GetAllUsers();
        UserModel SearchForId(int id);
        UserModel Add(UserModel usuario);
        UserModel Update(UserModel usuario);

        //UserModel RenewPassword(UserModel usuario, string key);
        bool Delete(int id);
    }
}
