using System.ComponentModel.DataAnnotations;

namespace valmet_cadastro_item.Models
{
 
        public class LoginModel
        {
            [Required(ErrorMessage = "Digite seu login")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Digite sua senha")]
            public string Password { get; set; }

        }
    
}
