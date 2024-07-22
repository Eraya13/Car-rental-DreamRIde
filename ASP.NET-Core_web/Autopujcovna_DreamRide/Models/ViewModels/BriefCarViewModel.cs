using System.ComponentModel.DataAnnotations;
namespace Autopujcovna_DreamRide.Models.ViewModels
{
    /// <summary>
    // ViewModel auta pro zobrazení náhledu auta (karty auta) pro metodu Delete() a Index() - kde se vypisuje pouze stručný náhled auta
    // Všechny informace o autě se zobrazí po rozkliknutí karty auta - s využitím metody Details()
    // <see> Tento model obsahuje jen několik parametrů, které jsou shodné s těmi v CarDetailsViewModel - pro více informací náhledněte na uvedený model </see>
    /// </summary>
    public class BriefCarViewModel
    {

        public int CarId { get; set; }

        [Display(Name = "Název auta")]
        public string Title { get; set; } = "";
        
        [Display(Name = "Motor: ")]
        public string Engine { get; set; } = "";

        [Display(Name = "Výkon: ")]
        public int Power { get; set; } = 0;
        
        [Display(Name = "Převodovka: ")]
        public string Transmission { get; set; } = "";

        [Display(Name = "Pohon: ")]
        public string DriveTrain { get; set; } = "";
        
        /// <summary>
        /// Celá relativní cesta k titulní fotce auta předané z atributů Car <param name="Car.ImageCarFolder"> + <param name="Car.TitleCarImage"> 
        /// </summary>
        [Display(Name = "Titulní fotka auta")]
        public string ImageFullPath { get; set; } = "";

        /// <summary>
        /// Parametrický konstruktor, který čerpá hodnoty z instance třídy Car
        /// </summary>
        public BriefCarViewModel(int id, string label, string model, string engineType, float engineDisplacement,
               int power, string transmission, string driveTrain, string titleCarImage)
        {
            CarId = id;
            Title = label + " " + model;
            Engine = engineType + " " + engineDisplacement.ToString("0.0");
            Power = power;
            Transmission = transmission;
            DriveTrain = driveTrain;
            ImageFullPath = Car.ImageCarFolder + titleCarImage;
        }
    }
}
