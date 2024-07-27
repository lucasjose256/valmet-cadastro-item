using Microsoft.EntityFrameworkCore;
using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Helper
{
    public class DataBase : DbContext
    {

        public DataBase(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> Usuarios { get; set; }


    }

}
