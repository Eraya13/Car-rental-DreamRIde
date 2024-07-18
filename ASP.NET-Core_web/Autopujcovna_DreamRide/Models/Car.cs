using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models
{
    public class Car
    {

        [Key]
        public int Id { get; set; }

        // Basic info
        [Required(ErrorMessage = "Značka auta musí být vyplněna")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Značka auta musí být zapsána pomocí zkratek např. BMW")]   // značka mezi 50 a 2 znaky
        [Display (Name = "Značka")]
        public string Label { get; set; } = "";
       
        [Required(ErrorMessage = "Model auta musí být vyplněn")]
        [StringLength(20)]
        public string Model { get; set; } = "";
        
        [Required(ErrorMessage = "Rok výroby musí být vyplněn")]
        [Range(1900, 3000, ErrorMessage = "Rok výroby musí být větší než 1899")]
        [Display(Name = "Rok výroby")]
        public int YearOfManufacture { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Volba typu auta je povinná")]
        [Display(Name = "Typ karosérie")]
        public string TypeOfCar { get; set; } = "";     // e.g. Sedan, Coupé, Limusine, Crossover, Hatchback, Cabriolet,

        [StringLength(25)]
        [Required(ErrorMessage = "Volba typu paliva je povinná")]
        [Display(Name = "Palivo")]
        public string Fuel { get; set; } = "";

        // Specifikace motoru
        [StringLength(20)]
        [Required(ErrorMessage = "Typ motoru musí být vyplněn")]
        [Display(Name = "Označení motoru")]
        public string EngineType { get; set; } = "Unknown"; // typ motoru - e. g. V8, V6, B13
        
        [Display(Name = "Objem motoru")]
        public float? EngineDisplacement { get; set; } // objem motoru v l

        [Range(1, 5000, ErrorMessage = "Výkon v kW musí být v rozsahu 1 - 5000 kW")]
        [Display(Name = "Výkon v kW")]
        public int PowerInKw { get; set; }

        // Další technické údaje
        [Required(ErrorMessage = "Volba typu převodovky je povinná")]
        [Display(Name = "Převodovka")]
        public string Transmission { get; set; } = "";   // převodovka

        // Typ pohonu
        // all values: FWD (front), RWD (Rear), 4x4, AWD
        [StringLength(5)]
        [Required(ErrorMessage = "Volba typu pohonu je povinná")]
        [Display(Name = "Pohon")]
        public string DriveTrain { get; set; } = "";

        [Range(1, 500, ErrorMessage = "Maximální rychlost musí být v rozsahu 1 - 700")]
        [Display(Name = "Max rychlost (km/h)")]
        public int TopSpeedKmForHour { get; set; }

        


        // Navigační vlastnost pro Requests
        public ICollection<Request> ?Requests { get; set; }
    }
}
