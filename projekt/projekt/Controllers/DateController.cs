using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

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
            DateOnly date=DateOnly.Parse(czas);
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var currentDate = date.AddDays(-1);
            var wyniki = _context.Konta
                .Where(a => a.Nazwa == userName)
                .Include(acc => acc.Jadlospisy.Where(a => a.Dzień == currentDate))
                .ThenInclude(jadlo => jadlo.Dania)
                .ThenInclude(danie => danie.DaniaProdukty)
                .ThenInclude(dapro => dapro.Produkty);
            return View("../Account/Index",wyniki);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
