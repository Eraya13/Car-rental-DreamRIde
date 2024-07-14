using System.ComponentModel.DataAnnotations;


namespace Autopujcovna_DreamRide.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Značka auta musí být vyplněna")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Značka auta musí být zapsána pomocí zkratek např. BMW")]   // značka mezi 50 a 2 znaky
        public string Label { get; set; } = "";
        [Required(ErrorMessage = "Model auta musí být vyplněn")]
        [StringLength(20)]
        public string Model { get; set; } = "";
        [Required(ErrorMessage = "Rok výroby musí být vyplněn")]
        [Range(1900, 3000, ErrorMessage = "Rok výroby musí být větší než 1899")]
        public int YearOfManufacture { get; set; }
        [StringLength(25)]
        [Required(ErrorMessage = "Volba typu auta je povinná")]
        public string TypeOfCar { get; set; } = "";     // e.g. Sedan, Coupé, Limusine, Crossover, Hatchback, Cabriolet,

        [StringLength(20)]
        [Required(ErrorMessage = "Typ motoru musí být vyplněn")]
        public string EngineType { get; set; } = "Unknown"; // typ motoru - e. g. V8, V6, B13
        public float? EngineDisplacement { get; set; } // objem motoru v l
        [Required(ErrorMessage = "Volba typu převodovky je povinná")]
        public string Transmission { get; set; } = "";   // převodovka

        [Range(1, 500, ErrorMessage = "Maximalní rychlost musí být v rozsahu 1 - 700")]
        public int TopSpeedKmForHour { get; set; }
        [Range(1, 5000, ErrorMessage = "Výkon v kW musí být v rozsahu 1 - 5000 kW")]
        public int PowerInKw { get; set; }
        [StringLength(25)]
        [Required(ErrorMessage = "Volba typu paliva je povinná")]
        public string Fuel { get; set; } = "";


        // Navigační vlastnost pro Requests
        public ICollection<Request> ?Requests { get; set; }
    }
}
