using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;  // Pro použití metody GetRequiredService
using Autopujcovna_DreamRide.Models;



namespace Autopujcovna_DreamRide.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AllowedUsernames allowedUsernames; // Přidáme zde pole pro povolená uživatelská jména


        public AccountController
            (UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             AllowedUsernames allowedUsernames)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.allowedUsernames = allowedUsernames;
        }



        // Prvotní registrace - form zobrazení (pokus)
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // Registrace pokud vše ok, jinak se vrací registrační formulář s přijatým modelem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.ViewModels.RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Ověření, zda zadané uživatelské jméno patří mezi povolená
                if (model.Username == allowedUsernames.AdminUsername || model.Username == allowedUsernames.ManagerUsername)
                {
                    var user = new IdentityUser { UserName = model.Username };
                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        if (model.Username == allowedUsernames.AdminUsername)
                        {
                            await userManager.AddToRoleAsync(user, UserRoles.Admin);
                        }
                        else if (model.Username == allowedUsernames.ManagerUsername)
                        {
                            await userManager.AddToRoleAsync(user, UserRoles.RequestManager);
                        }

                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }

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

            return View(model);
        }



        private IActionResult RedirectToLocal(string? returnUrl)        // přesměruje uživatele na URL adresu
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // Přesměruje uživatele podle role na příslušnou domovskou stránku (Admin -> Cars -> Index; Manager -> Request -> Index)
        // Prvotní verze vždy přesměruje na Cars -> Index
        private IActionResult RedirectHome()
        {
            return RedirectToAction(nameof(CarsController.Index), "Cars");
        }

        // prvotní zobrazení LoginViewu
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;  // zapamatování URL, kam uživatele vrátit po úspěšném přihlášení
            return View();
        }




        // odeslání formuláře Loginu --- zpracování pokusu o přihlášení
        [HttpPost]
        [ValidateAntiForgeryToken]      // autorizační token proti CSRF útokům - generuje se pro každou akci uživatele jiný (ověření)
        public async Task<IActionResult> Login(Models.ViewModels.LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result =     // uvedení SignInResult z určitého jm. prostoru
                    await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);      // přihlásí uživatele
                // ověří se, zda je heslo a login (email) true
                // hodnoty: login, heslo, zapamatování přihlášení, uzamknutí účtu po neuspěšném pokusu o přihlášení

                // dokud se neprovede úspěšný login - vracíme uživateli formulář...
                //SignInResult -- signalizuje úspěšnost přihlášení... -> přesměrování uživatele (RedirectToLocal())
                
                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);

                ModelState.AddModelError("Login error", "Neplatné přihlašovací údaje.");
                return View(model);
            }

            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři
            return View(model);
        }


        // odhlášení uživatele
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToLocal(null);       // návrat na domovskou stránku
        }
    }
}
