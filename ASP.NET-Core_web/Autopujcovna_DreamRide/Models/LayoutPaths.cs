using Microsoft.Extensions.Primitives;

namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Třída představující relativní cesty k použitým rozložení jednotlivých podstránek - tzn. to, co by měli jednotlivé podstránky mít a nemít společné
    /// např. Správa nabízených aut autopůjčovny (Car Management of Dream Ride rental) má specifickou navigaci v headeru a nemá (nepotřebuje mít footer s dalšími odkazy na stránky)
    /// ---> Výchozí Layout zde nemá uvedenou cestu, jelikož je použit pokud se nastanoví u podstránky Layout
    /// <see cref="_Layout" or cref="_ViewStart" />
    /// </summary>
    public static class LayoutPaths
    {
        /// <summary>
        /// Layout pro správu nabízených aut autopůjčovny Dream Ride - tzv. prostředí určeno pouze pro administrátora
        /// </summary>
        public const string CarManagement = "~/Views/Shared/_CarManagement.cshtml";

        /// <summary>
        /// Layout pro registraci či přihlášení uživatele do správy žádostí a správy nabízených aut
        /// Tento Layout neobsahuje žádný header ani footer - pouze definuje základní kraje obsahu pro podstránky
        /// </summary>
        public const string LoginLayout = "~/Views/Shared/_LoginLayout.cshtml";

        /// <summary>
        /// Layout pro domovskou (hlavní) stránku autopůjčovny Dream Ride
        /// Tento Layout obsahuje header i footer, avšak neobsahuje header pro podstránku s nadpisem konkrétní stránky
        /// např. Nabízená auta či Audi TT (název stránky, který zobrazuje podrobnosti o tomto autě)
        /// </summary>
        public const string HomeLayout = "~/Views/Shared/_HomeLayout.cshtml";
    }
}
