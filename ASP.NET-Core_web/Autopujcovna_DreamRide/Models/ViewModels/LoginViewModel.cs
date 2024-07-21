using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models.ViewModels
{
    /// <summary>
    /// ViewModel pro formulář sloužící k přihlášení uživatele
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Uživatelské jméno
        /// </summary>
        [Required(ErrorMessage = "Zadejte uživatelské jméno")]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; } = "";

        /// <summary>
        /// Heslo k přihlášení
        /// </summary>
        [Required(ErrorMessage = "Zadejte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        /// <summary>
        /// Informace pro prohlížeč, zda má zapamatovat jeho údaje, aby uživatel při další návštěvě nemusel být nucen se znovu přihlašovat
        /// </summary>
        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
