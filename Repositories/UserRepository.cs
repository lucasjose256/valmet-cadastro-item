using valmet_cadastro_item.Helper;
using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBase _context;

        public UserRepository(DataBase dataBase)
        {
            _context = dataBase;
        }
        public UserModel Add(UserModel user)
        {
            user.DateRegister = DateTime.Now;
            user.SetHash();
            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            UserModel usuarioDB = SearchForId(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na deleção");


            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();
            return true;
        }

        public List<UserModel> GetAllUsers()
        {
            return _context.Usuarios.ToList();

        }

        public UserModel SearchForEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public UserModel SearchForId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UserModel Update(UserModel usuario)
        {
            UserModel usuarioDB = SearchForId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização");

            usuarioDB.Name = usuario.Name;

            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DateUpdate = DateTime.Now;
            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();
            return usuarioDB;
        }
    }
}
