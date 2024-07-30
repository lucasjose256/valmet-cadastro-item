using System.Security.Cryptography;
using System.Text;

namespace valmet_cadastro_item.Helper
{
    public static class PasswordHasher
    {

        public static string CreateHash(this string password)
        {
            var hash = SHA256.Create();
            var encoding=new ASCIIEncoding();
            var array= encoding.GetBytes(password);

            array=hash.ComputeHash(array);
            var strHex= new StringBuilder();

            foreach(var item in array)
            {

                strHex.Append(item.ToString("x2"));
            }
            return strHex.ToString();
        }
    }

}
