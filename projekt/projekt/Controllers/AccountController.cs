

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
            ViewData["Date"] = DateOnly.FromDateTime(DateTime.Now);
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var data = _context.Jadlospis;

            // var dat = currentDate.AddDays(-1);
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            ViewData["Date"] = currentDate;
            var jadlo = _context.Jadlospis.Where(a => a.IdKonta == idKOnta.Id && a.Dzień == currentDate);

            var danie = jadlo.Include(a => a.Dania);
            var danieProdkut = danie.ThenInclude(a => a.DaniaProdukty);
            var produkt = danieProdkut.ThenInclude(a => a.Produkty);
            Console.Write(danie);

            var wynik = _context.Jadlospis
               .Include(a => a.Dania.DaniaProdukty)
                       .ThenInclude(dapro => dapro.Produkty);



            return View(wynik);
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
                TempData["success"] = "nie znaleziono użytkownika";
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
