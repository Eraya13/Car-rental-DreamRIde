using System.ComponentModel.DataAnnotations;
namespace Autopujcovna_DreamRide.Models.ViewModels
{
    public class BriefCarViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Název auta")]
        public string Title { get; set; } = "";
        
        [Display(Name = "Motor ")]
        public string Engine { get; set; } = "";
        
        [Display(Name = "Převodovka ")]
        public string Transmission { get; set; } = "";

        [Display(Name = "Pohon: ")]
        public string DriveTrain { get; set; } = "";

        public BriefCarViewModel(int id, string label, string model, string engineType, float? engineDisplacement, string transmission, string driveTrain)
        {
            Id = id;
            Title = label + " " + model;
            Engine = $"{engineType} ({engineDisplacement?.ToString("0.0")})";
            Transmission = transmission;
            DriveTrain = driveTrain;
        }
    }
}
