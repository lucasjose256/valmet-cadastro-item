
    namespace valmet_cadastro_item.Models {
        public class ChangePasswordModel

        {
            public string Email { get; set; } // Optional, only if you need to display it
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
