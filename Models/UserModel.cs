using valmet_cadastro_item.Enums;

namespace valmet_cadastro_item.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ProfileEnum Perfil { get; set; }
        public string Password { get; set; }

        public DateTime DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }

        public bool SenhaValida(string password)
        {

            return Password == password;

        }




    }
}
