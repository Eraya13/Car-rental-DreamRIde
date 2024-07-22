using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Autopujcovna_DreamRide.Models;

namespace Autopujcovna_DreamRide.Controllers
{
    /// <summary>
    /// Controller sloužící k přihlašování správců webové stránky autopůjčovny (admin a správce žádostí)
    /// Spravují zejména stěžejního obsah webové stránky -> Auta a žádosti klientů
    /// Hlavní metody tohoto Controlleru jsou přihlášení, registrace a odhlášení uživatele
    /// Místo bezpečnostního ověřování pomocí emailu pro registraci používá dvě povolená uživatelská jména (AllowedUsernames)
    ///     K těmto povoleným Usernames jsou přiřazeny následně role (nastavuje se v Program.cs)
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Správce uživatelů pro operace související s uživateli
        /// </summary>
        private readonly UserManager<IdentityUser> userManager;
        
        /// <summary>
        /// Správce přihlášení pro operace související s přihlášením a odhlášením uživatelů
        /// </summary>
        private readonly SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Povolená uživatelská jména pro přístup do správy webové stránky
        /// </summary>
        private readonly AllowedUsernames allowedUsernames;

        /// <summary>
        /// Inicializuje novou instanci třídy <see cref="AccountController"/> s danými správci uživatelů, přihlášení a povolenými uživatelskými jmény.
        /// </summary>
        /// <param name="userManager">Správce uživatelů pro operace související s uživateli.</param>
        /// <param name="signInManager">Správce přihlášení pro operace související s přihlášením a odhlášením uživatelů.</param>
        /// <param name="allowedUsernames">Povolená uživatelská jména pro přístup do systému.</param>
        public AccountController
            (UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             AllowedUsernames allowedUsernames)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.allowedUsernames = allowedUsernames;
        }

        /// <summary>
        /// Zobrazuje registrační formulář
        /// </summary>
        /// <remarks>
        /// Uživatel si v pohledu pro zjednodušení pouze nastavuje heslo ke svému uživatelskému jménu (username), které zná
        /// Nastavení hesla lze provést pouze jednou
        /// </remarks>
        /// <returns>Pohled pro registraci uživatele.</returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Zpracovává registraci nového uživatele
        ///     Úspěšná registrace: přesměrování na domovskou stránku - prozatím CarsControlleru - "Nabídka aut"
        ///     Neúspěšná registrace: <returns> Formulář pro registraci uživatele s chybovými hláškami a přijatým modelem</returns> 
        /// </summary>
        /// <param name="model">Model obsahující údaje z registračního formuláře</param>
        /// <returns>Pohled pro registraci uživatele nebo přesměrování na domovskou stránku</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.ViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Ověření, zda zadané uživatelské jméno patří mezi povolená
                if (model.Username == allowedUsernames.AdminUsername || model.Username == allowedUsernames.ManagerUsername)
                {
                    var user = new IdentityUser { UserName = model.Username };
                    var result = await userManager.CreateAsync(user, model.Password);       

                    if (result.Succeeded)
                    {   
                        // Přiřazení uživateli roli na základě jeho "Username"
                        if (model.Username == allowedUsernames.AdminUsername)
                        {
                            await userManager.AddToRoleAsync(user, UserRoles.Admin);
                        }
                        else if (model.Username == allowedUsernames.ManagerUsername)
                        {
                            await userManager.AddToRoleAsync(user, UserRoles.RequestManager);
                        }

                        // Přihlášení uživatele
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectHome();
                    }

                    // Přidání chybových zpráv do ModelState, pokud uživatel nebyl úspěšně vytvořen
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Zadané uživatelské jméno není povolené.");
                }
            }

            // Vrácení pohledu s modelem, pokud někde došlo k chybě
            return View(model);
        }

        /// <summary>
        /// Zobrazuje přihlašovací formulář s možností zapamatování přihlášení
        /// </summary>
        /// <returns>Pohled pro přihlášení uživatele</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Zpracovává pokus o přihlášení uživatele.
        ///     Úspěšné přihlášení: přesměruje uživatele na domovskou stránku
        ///     Neúspěšné přihlášení: <returns> Formulář pro přihlášení uživatele s chybovými hláškami a přijatým modelem</returns> 
        /// </summary>
        /// <param name="model">Model obsahující údaje z přihlašovacího formuláře</param>
        /// <param name="returnUrl">Nepovinná URL adresa pro přesměrování po úspěšném přihlášení</param>
        /// <returns>Pohled pro přihlášení uživatele nebo přesměrování na domovskou stránku</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]      // autorizační token proti CSRF útokům - generuje se pro každou akci uživatele jiný
        public async Task<IActionResult> Login(Models.ViewModels.LoginViewModel model, string? returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                // Přihlášení uživatele pomocí SignInManager
                Microsoft.AspNetCore.Identity.SignInResult result =  
                    await signInManager.PasswordSignInAsync(        // Uživatelské jméno, heslo, Zapamatovat přihlášení
                        model.Username, 
                        model.Password, 
                        model.RememberMe, false);

                // Kontrola, zda bylo přihlášení úspěšné
                if (result.Succeeded)
                    return RedirectHome();      // Přesměrování na domovskou stránku (Admin: CarsController -> Index)

                // Přidání chybové zprávy do ModelState, pokud přihlášení nebylo úspěšné
                ModelState.AddModelError("Login error", "Neplatné přihlašovací údaje.");
                return View(model);
            }

            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři
            return View(model);
        }

        /// <summary>
        /// Odhlásí aktuálně přihlášeného uživatele.
        /// </summary>
        /// <returns>Přesměrování na úvodní stránku.</returns>
        public async Task<IActionResult> Logout()
        {
            // Odhlášení uživatele pomocí SignInManager
            await signInManager.SignOutAsync();
            return RedirectToLocal(null);       // Přesměrování na úvodní stránku webové stránky -> HomeController -> Index
        }

        /// <summary>
        /// Přesměruje uživatele na zadanou URL adresu, pokud je lokální.
        /// Pokud URL adresa není lokální, přesměruje na úvodní stránku.
        /// </summary>
        /// <param name="returnUrl">URL adresa, na kterou se má uživatel přesměrovat.</param>
        /// <returns>Přesměrování na zadanou URL adresu nebo na úvodní stránku.</returns>
        private IActionResult RedirectToLocal(string? returnUrl)        // přesměruje uživatele na URL adresu
        {
            // Kontrola, zda je URL adresa lokální
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else     // Přesměrování na úvodní stránku, pokud URL adresa není lokální
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Přesměruje uživatele podle role na příslušnou domovskou stránku
        /// Prvotní verze vždy přesměruje na Cars -> Index
        /// Finální verze: RequestManager bude přesměrován na Request -> Index
        /// </summary>
        /// <returns>Přesměrování na stránku Cars -> Index.</returns>
        private IActionResult RedirectHome()
        {
            return RedirectToAction(nameof(CarsController.Index), "Cars");
        }
    }
}
