using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Třída Request reprezentuje tabulku [Requests] v databázi
    /// Její instance jsou žádosti od klientů, kteří si chtějí půjčit auto z imaginární půjčovny Dream Ride
    /// </summary>
    public class Request
    {
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Cizí klíč - identifikátor - klienta, který vytvořil tuto žádost a žádá o půjčení nějakého auta
        /// </summary>
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        
        /// <summary>
        /// Cizí klíč - identifikátor - auta, které si klient v žádosti chce půjčit
        /// </summary>
        [ForeignKey("CarId")]
        public int CarId { get; set; }

        /* public string RentalType { get; set; }       plánovaný atribut, který by zachycoval o jaký typ půjčení auta má klient zájem
            /* Jednalo by se např. o zážitkovou jízdu na okruhu s profesionálním řidičem, samostatná jízda pouze s instrukcí lektora, klasické půjčení auta */

        /// <summary>
        /// Dodatečné informace, které chce klient sdělit k vypůjčení auta
        /// </summary>
        [StringLength (500, ErrorMessage = "Zpráva nesmí být delší než 500 znaků")]
        public string AdditionalInfo { get; set; } = "";
        
        /// <summary>
        /// Preferovaná forma kontaktu - tzn. jakou komunikaci klient upřednostňuje
        /// </summary>
        [StringLength (50)]
        [Required]
        public string PrefferedContactWay { get; set; } = "";

        /// <summary>
        ///  Předpokládaný den, kdy si klient chce vypůjčit auto
        /// </summary>
        [Required (ErrorMessage = "Je nutné zvolit předpokládaný datum vypůjčení auta")]
        [DataType(DataType.Date)]       // pouze datum nikoliv čas
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDay { get; set; }


        /// <summary>
        /// Předpokládaný den, kdy by klient chtěl vrátit auto
        /// </summary>
        [Required(ErrorMessage = "Je nutné zvolit předpokládaný datum vrácení auta")]
        [DataType(DataType.Date)]       // pouze datum nikoliv čas
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDay { get; set; }

        /// <summary>
        /// Stav žádosti klienta o půjčení auta
        /// </summary>
        [StringLength(20)]
        public string Status { get; set; } = "Nová";
    }
}
