using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Konto acc)
        {
           
            Console.WriteLine(acc.Nazwa + " " + acc.Haslo);
            return View("wf");
        }



        //Register



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
    }
}
