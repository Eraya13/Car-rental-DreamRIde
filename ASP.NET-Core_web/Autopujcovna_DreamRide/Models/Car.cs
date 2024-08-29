using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Třída Client reprezentuje tabulku [Cars] v databázi
    /// Její instance jsou nabízená auta k půjčení imaginární půjčovny Dream Ride
    /// </summary>

    public class Car
    {

        [Key]
        public int Id { get; set; }

        /* Obecné atributy vozidla */

        /// <summary>
        /// Značka auta
        /// </summary>
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Značka auta musí být zapsána pomocí zkratek např. BMW")]
        [Required(ErrorMessage = "Značka auta musí být vyplněna")]
        [Display (Name = "Značka")]
        public string Label { get; set; } = "";
       
        /// <summary>
        /// Model auta
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "Model auta musí být vyplněn")]
        public string Model { get; set; } = "";

        /// <summary>
        /// Rok výroby auta
        /// </summary>
        [Range(1900, 9999, ErrorMessage = "Rok výroby musí být větší než 1899")]
        [Required(ErrorMessage = "Rok výroby musí být vyplněn")]
        [Display(Name = "Rok výroby")]
        public int YearOfManufacture { get; set; }

        /// <summary>
        /// Karosérie auta či Typ auta
        /// </summary>
        [StringLength(25)]
        [Required(ErrorMessage = "Volba typu karosérie je povinná")]
        [Display(Name = "Karosérie")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "The field cannot contain numbers.")]   // // kontroluje přítomnost čísel - zde čísla nesmí být přítomna
        public string Body { get; set; } = "";

        /// <summary>
        /// Palivo, na které jezdí auto
        /// </summary>
        [StringLength(25)]
        [Required(ErrorMessage = "Volba typu paliva je povinná")]
        [Display(Name = "Palivo")]
        public string Fuel { get; set; } = "";

        /// <summary>
        /// Maximální výrobcem daná rychlost km/h
        /// </summary>
        [Range(1, 500, ErrorMessage = "Maximální rychlost musí být v rozsahu 1 - 700")]
        [Required(ErrorMessage = "Maximální rychlost musí být vyplněna")]
        [Display(Name = "Max rychlost (km/h)")]
        public int TopSpeedKmForHour { get; set; }

        /* Specifikace motoru */

        /// <summary>
        /// Typ motoru či jeho pojmenování výrobcem
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "Typ motoru musí být vyplněn")]
        [Display(Name = "Typ motoru")]
        public string EngineType { get; set; } = "";   // typ motoru je např. V8, V6, B13, H-4

        /// <summary>
        /// Objem motoru (zaokrouhlený) v litrech
        /// </summary>
        [Required (ErrorMessage = "Objem motoru musí být vyplněn")]
        [Display(Name = "Objem motoru")]
        public float EngineDisplacement { get; set; } = 999;

        /// <summary>
        /// Výkon auta v kW
        /// </summary>
        [Range(1, 5000, ErrorMessage = "Výkon v kW musí být v rozsahu 1 - 5000 kW")]
        [Required(ErrorMessage = "Maximální rychlost musí být vyplněna")]
        [Display(Name = "Výkon v kW")]
        public int PowerInKw { get; set; }

        /* Další technické údaje */

        /// <summary>
        /// Pohon nápravy auta
        /// </summary>
        [StringLength(5)]
        [Required(ErrorMessage = "Volba typu pohonu je povinná")]
        [Display(Name = "Pohon")]
        public string DriveTrain { get; set; } = "";
        
        /// <summary>
        /// Převodovka
        /// </summary>
        [Required(ErrorMessage = "Volba typu převodovky je povinná")]
        [Display(Name = "Převodovka")]
        public string Transmission { get; set; } = "";

        /*Komentář k řešení uložení názvu fotky a samotnému nahrávní do vymezené složky: Je mi zcela jasné, že to není ideální řešení - plánuji tuto funkcionalitu později vyřešit -
        z časových důvodů je zobrazení titulní fotky pro konkrétní auto řešeno touto zjednodušenou "polodynamickou cestou" */

        /// <summary>
        /// Proměnná uchovávající relativní cestu složky, kde jsou uloženy či kam se mají uložit všechny titulní fotky jednotlivých aut
        /// </summary>
        [StringLength(100)]
        public const string ImageCarFolder = "~/images/cars/";

        /// <summary>
        /// Název titulní fotky, která náleží ke konkrétnímu autu  - všechna fotky aut se nachází v jedné složce - cesta k ní je zahrnuta v <param name="ImageCarFolder">
        /// </summary>
        [StringLength(100)]
        [RegularExpression(@".*\..*", ErrorMessage = "Název titulní fotky musí obsahovat příponu souboru - např obr.jpg")]
        [Required(ErrorMessage = "Je nutné zadat název titulní fotky ve složce \"~/images/Cars/\"")]
        [Display(Name = "Titulní fotka auta")]
        public string TitleCarImage { get; set; } = "";

        /// <summary>
        /// Navigační vlastnost Kolekce pro žádosti - pro specifikování, že existuje vazba mezi žádostí (request) a autem (car)
        /// </summary>
        public ICollection<Request> ?Requests { get; set; }

        /// <summary>
        /// Metoda, která vrácí základní informace o Autě
        /// </summary>
        /// <returns>Značka auta Model - např. Audi TT</returns>
        public override string ToString()
        {
            return $"{Label} {Model}";
        }
    }
}
