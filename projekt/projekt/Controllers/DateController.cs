namespace projekt.Controllers
{
    public class DateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DateController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult poprzedni(string czas)
        {
            // Wykonaj logikę na podstawie przekazanych danych
            // ...
            DateOnly date = DateOnly.Parse(czas);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var currentDate = date.AddDays(-1);
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            ViewData["Date"] = currentDate;
   //posilki
            //var jadlo = _context.Jadlospis.Where(a => a.IdKonta == idKOnta.Id && a.Dzień == currentDate);

           // var danie = jadlo.Include(a => a.Dania);
            //var danieProdkut = danie.ThenInclude(a => a.DaniaProdukty);
            //var produkt = danieProdkut.ThenInclude(a => a.Produkty);

          // main mial tutaj pusto, czyli bez tych 4 lini wyzej

            var wyniki = _context.Jadlospis
    .Include(a => a.Dania)
        .ThenInclude(danie => danie.DaniaProdukty)
            .ThenInclude(dapro => dapro.Produkty)
    .Where(a => a.IdKonta == idKOnta.Id && a.Dzień == currentDate)
    .ToList();

            return View("../Account/Index", wyniki);
        }
        public IActionResult nastepny(string czas)
        {
            // Wykonaj logikę na podstawie przekazanych danych
            // ...
            DateOnly date = DateOnly.Parse(czas);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var currentDate = date.AddDays(1);
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            ViewData["Date"] = currentDate;
            var jadlo = _context.Jadlospis.Where(a => a.IdKonta == idKOnta.Id && a.Dzień == currentDate);

            var danie = jadlo.Include(a => a.Dania);
            var danieProdkut = danie.ThenInclude(a => a.DaniaProdukty);
            var produkt = danieProdkut.ThenInclude(a => a.Produkty);
           if(currentDate == DateOnly.FromDateTime(DateTime.Now))
            {
                return RedirectToAction("Index", "Account");
            }

            var wyniki = _context.Jadlospis
.Include(a => a.Dania)
  .ThenInclude(danie => danie.DaniaProdukty)
      .ThenInclude(dapro => dapro.Produkty)
.Where(a => a.IdKonta == idKOnta.Id && a.Dzień == currentDate)
.ToList();
            return View("../Account/Index", wyniki);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WeekMeals()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateOnly endOfWeek = startOfWeek.AddDays(7);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            var jadlo = _context.Jadlospis.Where(j => j.Dzień >= startOfWeek && j.Dzień < endOfWeek && j.IdKonta == idKOnta.Id).Include(a => a.Dania).ThenInclude(a=>a.DaniaProdukty).ThenInclude(a=>a.Produkty).OrderBy(a => a.Dzień);
            @ViewData["Date"] = currentDate;
            return View(jadlo);
        }
        public IActionResult previousWeek(string czas)
        {
            DateOnly date = DateOnly.Parse(czas);
            DateOnly startOfWeek = date.AddDays(-(int)date.DayOfWeek);
            date = startOfWeek.AddDays(-1);
            startOfWeek = date.AddDays(-(int)date.DayOfWeek);

            DateOnly endOfWeek = startOfWeek.AddDays(7);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            var jadlo = _context.Jadlospis.Where(j => j.Dzień >= startOfWeek && j.Dzień < endOfWeek && j.IdKonta == idKOnta.Id).Include(a => a.Dania).ThenInclude(a => a.DaniaProdukty).ThenInclude(a => a.Produkty).OrderBy(a => a.Dzień);
            @ViewData["Date"] = startOfWeek;
            return View("WeekMeals",jadlo);
        }
        public IActionResult nextWeek(string czas)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly breakDate = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateOnly date = DateOnly.Parse(czas);
            DateOnly startOfWeek = date.AddDays(-(int)date.DayOfWeek);

            DateOnly endOfWeek = startOfWeek.AddDays(7);
            date = endOfWeek.AddDays(1);
            startOfWeek = date.AddDays(-(int)date.DayOfWeek);
             endOfWeek = startOfWeek.AddDays(7);
         
            if(startOfWeek <= breakDate && breakDate<endOfWeek)
            {
                
                return RedirectToAction("WeekMeals");
            }
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            var jadlo = _context.Jadlospis.Where(j => j.Dzień >= startOfWeek && j.Dzień < endOfWeek && j.IdKonta == idKOnta.Id).Include(a => a.Dania).ThenInclude(a => a.DaniaProdukty).ThenInclude(a => a.Produkty).OrderBy(a=>a.Dzień);
            @ViewData["Date"] = startOfWeek;
            return View("WeekMeals",jadlo);
        }
    }
}
