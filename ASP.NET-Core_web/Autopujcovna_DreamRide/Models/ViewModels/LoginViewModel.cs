using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zadejte uživatelské jméno")]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Zadejte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
