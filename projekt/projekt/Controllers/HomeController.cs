using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using System.Diagnostics;

namespace projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
    
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Produkt> produktLista = _db.Produkty.ToList();
            return View(produktLista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}