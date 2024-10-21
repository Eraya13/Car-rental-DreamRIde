***Stručný popis projektu***

Tento projekt je napsán v C# pomocí frameworku ASP.NET a využívá Entity Framework Core spolu s databází SQL Express (MS SQL). Projekt využívá principu Object First.

Jedná se o webovou aplikaci pro správu autopůjčovny Dream Ride, která běží pouze lokálně na vašem zařízení.



--------Konfigurace programu - co je třeba provést před zkompilování programu
!!Velmi důležité - jinak program nepoběží jak má a vyběhne výjimka
Pro správné fungování aplikace je nutné napojení na databázi. Toto napojení se nastavuje v souboru appsettings.json v sekci ConnectionStrings.

Příklad ConnectionString v appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DreamRideDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
Úprava ConnectionStrings: Ujistěte se, že jste změnili hodnotu ConnectionStrings na správné nastavení vaší databáze. Výchozí hodnota je platná pouze pro mé zařízení.

***Založení databáze***: 

Je doporučeno, aby na místě databáze byla již založena databáze, ale není to nutné. Aplikace automaticky vytvoří databázi, pokud neexistuje.

!!! Je potřeba před zkompilováním programu ve Visual Studiu přejít do konzole nutnost NugettManageru (naleznete např. skrze Tools->NuGet Package Manager) zadat update-database
- provedou se tak všechny vytvořené migrace v projektu
(pro jistotu je dobré předtím také zkontrolovat, zda NuGetManager vidí migrace - get-migration)

Vše by pak mělo fungovat bez problémů!!

***Počáteční stav databáze***:

Počáteční databáze k tomuto projektu by neměla obsahovat žádné záznamy. Data se přidávají až při používání aplikace.

***Povolená uživatelská jména***:
V souboru appsettings.json jsou definována povolená uživatelská jména:

"AllowedUsernames": {
   "Admin": "Dream.Admin",
   "Manager": "Filip.Steinmetz"
}


---------------------***Po nastavení detailů výše***---------------------

***Registrace testovacích uživatelů***:

Otevřete webový prohlížeč a přejděte na adresu /Account/Register.
Zaregistrujte uživatele s povoleným uživatelským jménem:
	Admin: Dream.Admin
	Manager: Filip.Steinmetz
Po registraci můžete otestovat funkcionalitu přihlášených uživatelů - admina a správce.
Pro jiná uživatelská jména nebude možné provést registraci a následné přihlášení!

Funkcionalita pro tyto dvě uživatelské role jsou na CarsControlleru - správy aut (CRUD operace).

Pro přihlášení je nutné přejít na adresu /Account/Login.

***Použití fotek aut***:

Všechny fotky pro auta, které může admin použít pro tvorbu záznamů aut, jsou umístěny ve složce ~/Autopujcovna_DreamRide/wwwroot/images/cars.
Při tvorbě nebo editaci záznamu auta admin zadává pouze název obrázku, například ve tvaru audi-tt.png.

Testovací data pro 1 záznam auta: (zcela funkční)

Značka: Audi
Model: TT
Rok výroby: 2012
Karosérie: Kupé
Palivo: Benzín
Max rychlost (km/h)
Typ motoru: TSFI
Objem motoru: 2.0
Výkon v kW: 155
Převodovka: Manuální
Pohon: FWD
Titulní fotka auta: audi-tt.png


Tento text by měl poskytnout základní informace a instrukce pro nastavení a spuštění projektu Autopůjčovny Dream Ride.