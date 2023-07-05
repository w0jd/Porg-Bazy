using Microsoft.AspNetCore.Mvc;
using projekt.Models;

namespace projekt.Controllers
{
    public class UserMealsController:Controller
    {
        private readonly ApplicationDbContext _context;
        public UserMealsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult DeleteDayMeal(int id)
        {
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var usr = _context.Jadlospis.First(a=>a.IdJadlospis==id);
            var danie = _context.Jadlospis.Find(usr.IdDania, usr.IdKonta,usr.IdJadlospis);

            _context.Remove(danie);
            _context.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public IActionResult AddExistingMealPost(int danie, string czas) {
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var wyniki = _context.Konta
               .First(a => a.Nazwa == userName);
            Jadlospis jadlospis = new Jadlospis();
            DateOnly data = new DateOnly();
            czas = TempData["czas"].ToString();
            DateOnly.TryParse(czas, out data);
            jadlospis.Dzień = data;
            jadlospis.IdDania = danie;
            jadlospis.IdKonta = wyniki.Id;
            _context.Add(jadlospis);
            _context.SaveChanges();
            return RedirectToAction("Index","Account");
            //return View("../Account/Index");
        }
        public IActionResult AddExistingMeal(string id)
        {
           //DateOnly dat;
            id=id.Replace("%2F", "/");
            /*            DateOnly.TryParse(id, out dat);
                        Console.WriteLine(dat);*/
            ViewData["user"] = User.FindFirst(ClaimTypes.Name).Value;
            var wyniki = _context.Konta
               .Include(acc => acc.Jadlospisy)
               .ThenInclude(jadlo => jadlo.Dania)
               .ThenInclude(dapr => dapr.DaniaProdukty)
               .ThenInclude(pr => pr.Produkty);
            ViewData["id"] = id;
            return View(wyniki);
        }
        public IActionResult Index()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var userName = User.FindFirst(ClaimTypes.Name).Value;

            var wyniki = _context.Konta
                .Where(a => a.Nazwa == userName)
                .Include(acc => acc.Jadlospisy)
                .ThenInclude(jadlo => jadlo.Dania)
                .ThenInclude(dapr=>dapr.DaniaProdukty)
                .ThenInclude(pr=>pr.Produkty);
            return View(wyniki);
        }
    }
}
