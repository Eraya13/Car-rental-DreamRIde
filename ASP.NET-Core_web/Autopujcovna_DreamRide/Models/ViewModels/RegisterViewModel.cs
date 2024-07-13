using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Zadejte emailovou adresu")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Zadejte heslo")]
        [StringLength(100, ErrorMessage = "{0} musí mít délku alespoň {2} a nejvíc {1} znaků.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Znovu zadejte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        [Compare(nameof(Password), ErrorMessage = "Zadaná hesla se neshodují.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
