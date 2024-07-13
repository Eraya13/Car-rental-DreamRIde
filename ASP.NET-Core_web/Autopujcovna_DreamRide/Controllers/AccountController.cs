using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace Autopujcovna_DreamRide.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController
            (UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        private IActionResult RedirectToLocal(string? returnUrl)        // přesměruje uživatele na URL adresu
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
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
                    await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);      // přihlásí uživatele
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
            {       // admin je user s admin právy
                IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };   // Vytvoření uživatele -> IdentityUser
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);     // isPersistent = určení, zda má být uživatel přihlašen po zavření prohlížeče
                    return RedirectToLocal(returnUrl);
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

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
