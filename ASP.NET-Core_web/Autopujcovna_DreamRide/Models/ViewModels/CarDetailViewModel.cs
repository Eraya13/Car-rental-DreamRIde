using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models.ViewModels
{
    public class CarDetailViewModel
    {
        public int CarId;

        [Display (Name = "Název auta")]
        public string FullName;

        [Display (Name = "Max rychlost")]
        public int TopSpeed { get; set; }

        [Display (Name = "Výkon")]
        public int Power { get; set; }
        
        [Display (Name = "Převodovka")]
        public string Transmission { get; set; }

        [Display(Name = "Rok výroby")]
        public int YearOfManufacture { get; set; }

        [Display(Name = "Motor")]
        public string Engine {  get; set; }

        [Display(Name = "Pohon kol")]
        public string DriveTrain { get; set; }

        [Display(Name = "Palivo")]
        public string Fuel {  get; set; }

        [Display(Name = "Karosérie")]
        public string Body { get; set; }

        public CarDetailViewModel(int carId, string label, string model, int topSpeed, int power,
                        string transmission, int yearOfManufacture, float displacement, string engineType, string driveTrain, string fuel, string body)
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
        }

    }
}
