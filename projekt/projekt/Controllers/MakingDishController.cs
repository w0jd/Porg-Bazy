using Microsoft.AspNetCore.Mvc;
using projekt.Models;

namespace projekt.Controllers
{
    public class MakingDishController : Controller
    {

     

        private readonly ApplicationDbContext _context;
        
        public MakingDishController(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public IActionResult Index()
        {
            IEnumerable<Produkt> produktLista = _context.Produkty.ToList();
            IEnumerable<Dania> daniaLista = _context.Dania.ToList();
            return View(produktLista);
        }
    }
}
