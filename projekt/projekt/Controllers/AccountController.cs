using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using projekt.Models;

namespace projekt.Controllers
{
    public class AccountController : Controller
    {
        /*
        //połączenie z bazą danych (do zmiany?)
        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        MySqlDataReader dr;
        void ConnectionString()
        {

        }*/

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Konto acc)
        {
            /*
            ConnectionString();
            con.Open();
            com.CommandText = "select * from users where username='"+acc.Nazwa+"' and password='"+acc.Haslo+"';";

            dr= com.ExecuteReader();
            
            Console.WriteLine(acc.Nazwa + " " + acc.Haslo);
            if (dr.Read())
            {
                con.Close();
            }
            else {
                con.Close();
                return View();
            }*/
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
        public ActionResult Register(Konto acc)
        {
            /*
            ConnectionString();
            con.Open();
            com.CommandText = "select * from users where username='"+acc.Nazwa+"' and password='"+acc.Haslo+"';";

            dr= com.ExecuteReader();
            
            Console.WriteLine(acc.Nazwa + " " + acc.Haslo);
            if (dr.Read())
            {
                con.Close();
            }
            else {
                con.Close();
                return View();
            }*/
            Console.WriteLine(acc.Nazwa + " " + acc.Haslo);
            return View("wf");
        }
    }
}
