using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jméno je povinný údaj")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Příjmení je povinný údaj")]
        [StringLength(100, ErrorMessage = "Rodné příjmení je dostačující")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email je povinný údaj")]
        [EmailAddress(ErrorMessage = "Zadaná adresa není platná")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Je nutné zvolit předvolbu")]
        public int MobilePreset { get; set; }

        [Required(ErrorMessage = "Je nutné vyplnit telefonní číslo")]
        [RegularExpression(@"^\d+(\s?\d+)*$", ErrorMessage = "Zadejte platné telefonní číslo.")]
        [StringLength(20)]      // omezení číslic
        public string MobileNumber { get; set; }
    }
}
