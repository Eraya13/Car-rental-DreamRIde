using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models.ViewModels
{
    /// <summary>
    /// Třída CarDetailViewModel slouží pro pohled Details = zobrazení jednotlivého vozidla
    /// Přijímá hodnoty z modelu Car
    /// </summary>
    public class CarDetailViewModel
    {
        /// <summary>
        /// Identifikátor odpovídajícího auta v tabulce [Cars]
        /// </summary>
        public int CarId;

        /// <summary>
        /// Název auta se skládá ze značky <param name="Car.Label"/> a modelu <param name="Car.Model"/>
        /// </summary>
        [Display(Name = "Název auta")]
        public string FullName;

        /// <summary>
        /// Maximální výrobcem stanovená rychlost km/h
        /// </summary>
        [Display(Name = "Max rychlost")]
        public int TopSpeed { get; set; }

        /// <summary>
        /// Rok výroby auta
        /// </summary>
        [Display(Name = "Rok výroby")]
        public int YearOfManufacture { get; set; }

        /// <summary>
        /// Výkon auta v kW
        /// </summary>
        [Display(Name = "Výkon")]
        public int Power { get; set; }

        /// <summary>
        /// Převodovka
        /// </summary>
        [Display(Name = "Převodovka")]
        public string Transmission { get; set; }

        /// <summary>
        /// Motor = string, který se skládá z <param name="Car.EngineType"/> + " " +<param name="Car.EngineDisplacement"/>
        /// </summary>
        [Display(Name = "Motor")]
        public string Engine { get; set; }

        /// <summary>
        /// Pohon nápravy auta
        /// </summary>
        [Display(Name = "Pohon nápravy")]
        public string DriveTrain { get; set; }

        /// <summary>
        /// Palivo, na které jezdí auto
        /// </summary>
        [Display(Name = "Palivo")]
        public string Fuel { get; set; }

        // <summary>
        /// Karosérie auta
        /// </summary>
        [Display(Name = "Karosérie")]
        public string Body { get; set; }
        
        /// <summary>
        /// Celá relativní cesta k titulní fotce auta předané z atributů Car <param name="Car.ImageCarFolder"> + <param name="Car.TitleCarImage"> 
        /// </summary>
        [Display(Name = "Titulní fotka auta")]
        public string ImageFullPath { get; set; } = "";

        /// <summary>
        /// Parametrický konstruktor, který čerpá hodnoty z instance třídy Car
        /// </summary>
        public CarDetailViewModel(int carId, string label, string model, int topSpeed, int power,
                        string transmission, int yearOfManufacture, float displacement, string engineType,
                        string driveTrain, string fuel, string body, string titleCarImage)
        {
            CarId = carId;
            FullName = label + " " + model;
            TopSpeed = topSpeed;
            Power = power;
            Transmission = transmission;
            YearOfManufacture = yearOfManufacture;
            Engine = displacement.ToString("0.0") + " " + engineType;
            DriveTrain = driveTrain;
            Fuel = fuel;
            Body = body;
            ImageFullPath = Car.ImageCarFolder + titleCarImage;
        }


    }
}
