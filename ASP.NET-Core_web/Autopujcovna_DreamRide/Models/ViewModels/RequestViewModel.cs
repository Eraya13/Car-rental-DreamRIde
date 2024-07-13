using System.ComponentModel.DataAnnotations;


namespace Autopujcovna_DreamRide.Models.ViewModels
{
    // třída, která slouží k tomu, aby pomohla s vytvořením žádosti, vytvořením klienta a přiřazení k žádosti příslušné auto dle volby klienta
    public class RequestViewModel
    {

        // Píšeme sem atributy Viewu, do kterých se má uložit nějaká hodnota proměnné z Viewu - odpovídá tedy políčkům formuláře
        // přiřazené auto k Request (zatím neřešit moc)
        public int? CarId { get; set; } = null;

        //  Request values:
        [Required(ErrorMessage = "Je nutné vybrat jednu z možností")]
        public string PrefferedContactWay { get; set; } = "";
        
        // Dobrovolné vyplnění
        [StringLength(500, ErrorMessage = "Zpráva nesmí být delší než 500 znaků")]
        public string Note { get; set; } = "";

        [Required(ErrorMessage = "Je nutné zvolit předpokládaný datum vypůjčení auta")]
        [DataType(DataType.Date)]       // pouze datum nikoliv čas
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDay { get; set; }

        [Required(ErrorMessage = "Je nutné vybrat počet dnů")]
        [Range(1, 31, ErrorMessage = "Vyberte počet dnů v rozmezí 1-31")]
        public int RentDays { get; set; }

        // Client values:
        [Required(ErrorMessage = "Jméno je povinný údaj")]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Příjmení je povinný údaj")]
        [StringLength(100, ErrorMessage = "Rodné příjmení je dostačující")]
        public string Surname { get; set; } = "";

        [Required(ErrorMessage = "Je nutné zvolit předvolbu")]
        public int PhonePreset { get; set; }

        [Required(ErrorMessage = "Je nutné vyplnit telefonní číslo")]
        [RegularExpression(@"^\d+(\s?\d+)*$", ErrorMessage = "Zadejte platné telefonní číslo.")]
        [StringLength(20)]      // omezení číslic
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Email je povinný údaj")]
        [EmailAddress(ErrorMessage = "Zadaná adresa není platná")]
        public string Email { get; set; } = "";
    }
}
