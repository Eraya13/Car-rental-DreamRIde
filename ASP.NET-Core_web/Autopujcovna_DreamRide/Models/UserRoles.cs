namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Třída zachycující uživatelské role, které mohou přihlášení uživatelé zastávat
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Role admina - plná práva: + read-write všeho ve webovém prostředí
        /// </summary>
        public const string Admin = "admin";

        /// <summary>
        /// Role správce žádostí - omezená práva:
        ///     + možnost vytváření a spravování žádostí o půjčení auta a změny stavu auta (např. stavy: půjčené, mimo provoz, volné)
        ///     - nemůže přidávat či odebírat auta z nabídky aut
        /// </summary>
        public const string RequestManager = "requestManager";
    }
}


