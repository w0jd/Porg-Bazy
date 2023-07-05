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
            var idKOnta = _context.Konta.First(a => a.Nazwa == userName);
            ViewData["Date"] = currentDate;
            var jadlo = _context.Jadlospis.Where(a => a.IdKonta == idKOnta.Id && a.Dzień == currentDate);

            var danie =jadlo.Include(a=>a.Dania);
            var danieProdkut = danie.ThenInclude(a => a.DaniaProdukty);
            var produkt = danieProdkut.ThenInclude(a => a.Produkty);
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
            Console.Write(produkt);

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
    }
}
