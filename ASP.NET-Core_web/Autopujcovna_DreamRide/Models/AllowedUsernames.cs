namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Třída představující jediná povolená uživatelská jména
    /// </summary>
    public class AllowedUsernames
    {
        /// <summary>
        /// Uživatelské jméno - "admin"
        /// </summary>
        public string ?AdminUsername { get; set; }

        /// <summary>
        /// Uživatelské jméno - "manager"
        /// </summary>
        public string ?ManagerUsername { get; set; }
    }
}
