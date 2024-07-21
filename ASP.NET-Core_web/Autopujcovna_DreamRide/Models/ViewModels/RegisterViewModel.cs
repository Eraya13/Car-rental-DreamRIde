using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models.ViewModels
{
    /// <summary>
    /// ViewModel sloužící k registraci nového uživatele
    ///     V mém projektu jsou povolená pouze 2 uživatelská jména, jelikož pro projekt nebylo implementováno ověření účtu při registraci.
    ///     Tento projekt pouze simuluje autopůjčovnu Dream Ride
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Uživatelské jméno, pod kterým se bude uživatel přihlašovat
        /// </summary>
        [Required(ErrorMessage = "Zadejte uživatelské jméno")]
        [Display(Name = "Username")]
        public string Username { get; set; } = "";

        /// <summary>
        /// Heslo uživatele k přihlášení
        /// </summary>
        [Required(ErrorMessage = "Zadejte heslo")]
        [StringLength(100, ErrorMessage = "{0} musí mít délku alespoň {2} a nejvíc {1} znaků.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        /// <summary>
        /// Heslo pro kontrolu - je nutné, aby se obě zadaná hesla shodovala
        /// </summary>
        [Required(ErrorMessage = "Znovu zadejte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        [Compare(nameof(Password), ErrorMessage = "Zadaná hesla se neshodují.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
