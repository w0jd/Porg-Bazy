

using projekt.Models;

namespace projekt.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }//połączenie z bazą
        public IActionResult Index()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var acc = _context.Konta.FirstOrDefault(a=>a.Nazwa==userName);
            var jadlo = _context.Jadlospis.FirstOrDefault(a => a.IdKonta == acc.Id && a.Dzień == currentDate) ;
            var danie = _context.Dania.Find( jadlo.IdDania);
            var dapro=_context.DaniaProdukty.FirstOrDefault(a=>a.IdDania==danie.Id);
            var produkt = _context.Produkty.Find(dapro.IdProduktu);
            var result = new ModelCalosci
            {
                Konto = acc,
                Jadlo = jadlo,
                Dania = danie,
                DaniaProdukty = dapro,
                Produkt = produkt
            };

            return View(result);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Verify(LoginModel request)
        {
            var user = _context.Konta.First(u => u.Nazwa == request.Nazwa);
            if (user == null)
            {
                TempData["success"] = "user not found";
                return RedirectToAction("Login", "Account");

            }
            if (user.Haslo != request.Haslo)
            {
                TempData["success"] = "złe hasło";
                return RedirectToAction("Login", "Account");
            }
            await SignInUser(user.Nazwa);
            var username = HttpContext.User.Identity.Name;
            return RedirectToAction("Index", "Home");
          /*  Console.WriteLine(acc.Nazwa + " " + acc.Haslo);
            return View("wf");*/
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Konto acc)
        {
            if (_context.Konta.Any(u => u.Nazwa == acc.Nazwa))
            {
                Console.WriteLine("Nazwa zajęta");
            }
            Console.WriteLine(acc.Nazwa + " " + acc.Haslo);
            _context.Konta.Add(acc);
            _context.SaveChanges();
            return View("Login");
        }
      //  [HttpPost("Login")]
     
        private async Task SignInUser(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),//tworzy coś w rodzaju obiektu o tych wartościach              
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);//tworzy obiekt reprezentujący tożsamość uzytkownika składjący się z jego nazwy i typu uwierzytenienia 
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));// zapisuje informacje o użytkownkiu w pliku cookie  claimsPrincipal reprezentuje identyfkacje i autoryzację użytkownika 
           
        }
    }
}
