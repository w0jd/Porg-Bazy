using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using projekt.Services;
using System.Collections.Generic;
using System.Linq;

namespace projekt.Controllers
{
    public class MealsController : Controller
    {

        private readonly IDataMealsService _dataMealsService;



        private readonly ApplicationDbContext _db;


        public MealsController(IDataMealsService DataProductService, ApplicationDbContext db)
        {
            _dataMealsService = DataProductService;


            _db = db;
        }
        public IActionResult Products(int pg = 1)
        {

            // var products = _dataMealsService.GetProducts();
            //return View(products);

            List<Produkt> produkty = _dataMealsService.GetProducts().ToList();
            const int pageSize = 25;
            if (pg < 1)
                pg = 1;
            int prodCount = produkty.Count();
            var pager = new Pager(prodCount, pg, pageSize);
            int prodSkip = (pg - 1) * pageSize;
            var data = produkty.Skip(prodSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);

        }
        public IActionResult NameMeal()
        {
            return View();
        }
        public IActionResult Add(string id)
        {
            string[] variables = id.Split('_');
            string idDania = variables[0];
            string idPorduktu = variables[1];
            int.TryParse(idDania, out int DanieId);
            int.TryParse(idPorduktu, out int PorduktuId);
            var i = ViewData["id"];
            var czy = _db.DaniaProdukty.Where(a => a.IdProduktu == PorduktuId && a.IdDania.ToString() == DanieId.ToString());
            if (czy.Count() == 0)
            {
                var dapro=new DaniaProdukty();
                dapro.IdProduktu = PorduktuId;
                string id_str = DanieId.ToString();
                int intid;
                ViewData["nazwa"] = DanieId;
                int.TryParse(id_str, out intid);
                dapro.IdDania = intid;
                dapro.Ilość = 1;
                _db.Add(dapro);
                _db.SaveChanges();

            }
            else
            {
                
            }
            // = DanieId;
            ViewData["nazwa"] = DanieId;
            var wyniki = _db.Produkty;
            return View("CreateMeal", wyniki);
        }

        public IActionResult CreateMealMethod(string nazwa)
        {
            var dan = new Dania();
            dan.NazwaDania= nazwa;
            var id_dania = _db.Dania.OrderBy(a => a.Id).Last();
            int plus1 = id_dania.Id;
           // dan.Id = plus1;
            _db.Dania.Add(dan);
            _db.SaveChanges();
            var id=_db.Dania.Where(d => d.NazwaDania.Equals(nazwa)).FirstOrDefault();
             id_dania = _db.Dania.OrderBy(a => a.Id).Last();
            ViewData["nazwa"]= plus1;
            var wyniki = _db.Produkty;
            return View("CreateMeal", wyniki);
        }
       
        public IActionResult MyMeals()
        {
            //var collection1 = GetCollection1Data();
            //var collection2 = GetCollection2Data();
            //var additionalData = GetAdditionalData();

            //var model = Tuple.Create(collection1, additionalData);

            //return View(model);

            List<List<decimal>> dania_detale_suma = new List<List<decimal>>();

            var products = _dataMealsService.GetProducts();
            var meals = _dataMealsService.GetMeals();

            foreach (var item in meals) {
                var meal_products = _dataMealsService.GetMealsDetails(item.Id);
                var list_ilosc = _dataMealsService.GetJadlospis(item.Id);
                List<decimal> suma_dla_dania = new List<decimal>();

                foreach (var product in meal_products)
                {
                    var ilosc = list_ilosc[0];
                    list_ilosc.RemoveAt(0);
                    if (suma_dla_dania.Count != 0)
                    {
                        suma_dla_dania[0] += (Convert.ToDecimal(product.Kaloryczność) * ilosc / 100);
                        suma_dla_dania[1] += (Convert.ToDecimal(product.Białko) * ilosc / 100);
                        suma_dla_dania[2] += (Convert.ToDecimal(product.Tłuszcz) * ilosc / 100);
                        suma_dla_dania[3] += (Convert.ToDecimal(product.Węglowodany) * ilosc / 100);
                        suma_dla_dania[4] += (Convert.ToDecimal(product.Błonnik) * ilosc / 100);
                    }
                    suma_dla_dania.Add(Convert.ToDecimal(product.Kaloryczność) * ilosc / 100);
                    suma_dla_dania.Add(Convert.ToDecimal(product.Białko) * ilosc / 100);
                    suma_dla_dania.Add(Convert.ToDecimal(product.Tłuszcz) * ilosc / 100);
                    suma_dla_dania.Add(Convert.ToDecimal(product.Węglowodany) * ilosc / 100);
                    suma_dla_dania.Add(Convert.ToDecimal(product.Błonnik) * ilosc / 100);


                }
                dania_detale_suma.Add(suma_dla_dania);
            }



            var products_meals = Tuple.Create(dania_detale_suma, meals);
            return View(products_meals);
        }

        [HttpGet]
        public IActionResult Details(int id) // id elementu ktory chcemy pobra z bazy danych
        {
            string nazwa = _dataMealsService.GetMealName(id);
            var meal_products = _dataMealsService.GetMealsDetails(id);
            var list_ilosc = _dataMealsService.GetJadlospis(id);


            var products_meals = Tuple.Create(meal_products, list_ilosc, nazwa);

            return View(products_meals);
        }

        [HttpGet]
        public IActionResult AddingMeals()
        {
            List<Produkt> produkty = _dataMealsService.GetProducts().ToList();
            var resault = Tuple.Create(produkty);
            return View(resault);
        }
        [HttpPost]
 /*       public IActionResult CreateMeal(string nazwaList<Produkt> produkty, List<decimal> ilosc)
        {
            //tutaj logika tworzenie dania 
            return RedirectToAction("MyMeals");
        }*/

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _dataMealsService.DeleteMeal(id);
            return RedirectToAction("MyMeals");
        }



        [HttpGet]
        public IActionResult Create()
        {
            
            Dania danie = new Dania();
            var daniaProdukty = new DaniaProdukty() { };
            danie.DaniaProdukty.Add(daniaProdukty);
            
            return View(danie);
        }
        [HttpPost]
        public IActionResult Create(Dania danie) {
            foreach (DaniaProdukty daniaprodukt in danie.DaniaProdukty) {
                if (daniaprodukt.IdDania == null || daniaprodukt.IdProduktu == null) {
                    danie.DaniaProdukty.Remove(daniaprodukt);
                }
            }


            _db.Add(danie);
            _db.SaveChanges();
            return RedirectToAction("MyMeals");

        }


    }
}
