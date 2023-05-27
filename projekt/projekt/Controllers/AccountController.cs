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

        }
        //koniec połączenia
        */
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            /*
            ConnectionString();
            con.Open();
            com.CommandText = "select * from users where username='"+acc.Name+"' and password='"+acc.Password+"';";
            dr= com.ExecuteReader();
            if(dr.Read())
            {
                con.Close();
                return View();
            }
            else {
                con.Close();
                return View();
            }
            */

            Console.WriteLine(acc.Name + " " + acc.Password);
            return View("wf");

            
        }
    }
}
