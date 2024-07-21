using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Třída Client reprezentuje tabulku [Clients] v databázi
    /// Její instance jsou klienti imaginární autopůjčovny Dream Ride,
    /// kteří si žádají o půjčení nějakého auta z aktuální nabídky aut 
    /// </summary>
    public class Client
    {
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Křestní jméno klienta
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Name { get; set; } = "";

        /// <summary>
        /// Rodné příjmení klienta
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = "";

        /// <summary>
        /// Kontaktní e-mailová adresa
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        
        /// <summary>
        /// Směrové telefonní číslo / telefonní předvolba
        /// Není ukládáno ono "+" předvolby - je pouze zobrazováno ve formuláři
        /// </summary>
        [Required]
        public int PhonePreset { get; set; }    // např. +420

        /// <summary>
        /// Telefonní (mobilní) číslo bez státní předvolby
        /// </summary>
        [Required]
        [RegularExpression(@"^\d+(\s?\d+)*$")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = "";

        /// <summary>
        /// Navigační vlastnost Kolekce pro žádosti - pro specifikování, že existuje vazba mezi žádostí o půjčení auta (request) a klientem (client)
        /// </summary>
        public ICollection<Request> ?Requests { get; set; }
    }
}
