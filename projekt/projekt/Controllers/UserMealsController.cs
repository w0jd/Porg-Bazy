using Microsoft.AspNetCore.Mvc;

namespace projekt.Controllers
{
    public class UserMealsController:Controller
    {
        private readonly ApplicationDbContext _context;
        public UserMealsController(ApplicationDbContext context)
        {
            _context = context;
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
