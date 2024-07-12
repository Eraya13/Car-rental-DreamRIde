using System.ComponentModel.DataAnnotations;

namespace Autopujcovna_DreamRide.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        [Required]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public int PhonePreset { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\s?\d+)*$")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = "";
    }
}
