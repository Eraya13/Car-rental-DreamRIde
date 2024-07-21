using System.ComponentModel.DataAnnotations;


namespace Autopujcovna_DreamRide.Models.ViewModels
{
    // Třída, která slouží k tomu, aby pomohla s vytvořením žádosti, vytvořením klienta a přiřazení k žádosti příslušné auto dle volby klienta
    public class RequestViewModel
    {

        // přiřazené auto k Request
        public int? CarId { get; set; } = null;

        [Required (ErrorMessage = "Je nutné si zvolit auto, které si chcete půjčit")]
        [Display (Name = "Auto")]
        public string CarName { get; set; }
        
        // Client values:
        [Required(ErrorMessage = "Jméno je povinný údaj")]
        [StringLength(100)]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Jméno nesmí obsahovat číslice.")]
        [Display(Name = "Jméno")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Příjmení je povinný údaj")]
        [StringLength(100, ErrorMessage = "Rodné příjmení je dostačující")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Příjmení nesmí obsahovat číslice.")]
        [Display(Name = "Příjmení")]
        public string Surname { get; set; } = "";

        [Required(ErrorMessage = "Je nutné zvolit předvolbu")]
        [Display (Name = "Telefonní číslo")]
        public int PhonePreset { get; set; }

        [Required(ErrorMessage = "Je nutné vyplnit telefonní číslo")]
        [RegularExpression(@"^\d+(\s?\d+)*$", ErrorMessage = "Zadejte platné telefonní číslo.")]
        [StringLength(20)]      // omezení číslic
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "E-mail je povinný údaj")]
        [EmailAddress(ErrorMessage = "Zadaná emailová adresa není platná")]
        [StringLength(100)]
        [Display (Name = "E-mail")]
        public string Email { get; set; } = "";

        //  Request values:
        [Required(ErrorMessage = "Je nutné vybrat jednu z možností")]
        [Display(Name = "Preferovaná forma komunikace")]
        public string PrefferedContactWay { get; set; } = "";
        
        // Dobrovolné vyplnění
        [StringLength(500, ErrorMessage = "Zpráva nesmí být delší než 500 znaků")]
        [Display (Name = "Doplňující informace")]
        public string AdditionalInfo { get; set; } = "";

        [Required(ErrorMessage = "Je nutné zvolit předpokládaný datum vypůjčení auta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display (Name = "Od")]
        public DateTime StartDay { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Je nutné zvolit předpokládaný datum návratu auta")]
        [DataType(DataType.Date)]       
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Do")]
        public DateTime EndDay { get; set; } = DateTime.Today;
    }
}
