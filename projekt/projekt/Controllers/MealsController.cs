using Microsoft.AspNetCore.Mvc.Rendering;
using projekt.Models;
using projekt.Services;
using projekt.ViewModels;

namespace projekt.Controllers
{
    public class MealsController : Controller
    {

        private readonly IDataMealsService _dataMealsService;

        //posilki
        // IEnumerable <Produkt> products;
        // public MealsController(IDataMealsService DataProductService)
        // {
        // _dataMealsService = DataProductService;
        // products = _dataMealsService.GetProducts();


        //}

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
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NazwaSortParam = sortOrder =="Nazwa" ? "nazwa_desc" : "Nazwa";
        //    ViewBag.KcalSort = sortOrder == "kcal" ? "kcal_desc" : "kcal";
        //    ViewBag.FatSort = sortOrder == "fat" ? "fat_desc" : "fat";
        //    ViewBag.FibreSort = sortOrder == "fibre" ? "fibre_desc" : "fibre";
        //    ViewBag.ProteinSort = sortOrder == "protein" ? "protein_desc" : "protein";
        //    ViewBag.CbhdSort = sortOrder == "cbhd" ? "cbhd_desc" : "cbhd";
        //    if (searchQuery != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchQuery = currentFilter;
        //    }

        //    ViewBag.currentFilter = searchQuery;

        //    var products = from p in _context.Produkty
        //                   select p;
        //    if (!String.IsNullOrEmpty(searchQuery))
        //    {
        //        products = products.Where(p => p.Nazwa.Contains(searchQuery));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "nazwa_desc":
        //            products= products.OrderByDescending(p=> p.Nazwa);
        //            break;
        //        case "protein":
        //            products=products.OrderBy(p=> p.Białko);
        //            break;
        //        case "protein_desc":
        //            products = products.OrderByDescending(p => p.Białko);
        //            break;
        //        case "kcal":
        //            products = products.OrderBy(p=> p.Kaloryczność);
        //            break;
        //        case "kcal_desc":
        //            products = products.OrderByDescending(p => p.Kaloryczność);
        //            break;
        //        case "fat":
        //            products = products.OrderBy(p => p.Tłuszcz);
        //            break;
        //        case "fat_desc":
        //            products = products.OrderByDescending(p => p.Tłuszcz);
        //            break;
        //        case "cbhd":
        //            products = products.OrderBy(p => p.Węglowodany);
        //            break;
        //        case "cbhd_desc":
        //            products = products.OrderByDescending(p => p.Węglowodany);
        //            break;
        //        case "fibre":
        //            products = products.OrderBy(p => p.Błonnik);
        //            break;
        //        case "fibre_desc":
        //            products = products.OrderByDescending(p => p.Błonnik);
        //            break;
        //        default:
        //            products = products.OrderBy(p => p.Nazwa);                  
        //            break;
        //    }
        //    int pageSize = 10;
        //    int pageNum = (page ?? 1);

        //    return View(products);}

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

            foreach (var item in meals)
            {
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
        public IActionResult CreateMeal(string nazwa/*List<Produkt> produkty, List<decimal> ilosc*/)
        {
            //tutaj logika tworzenie dania 
            return RedirectToAction("MyMeals");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _dataMealsService.DeleteMeal(id);
            return RedirectToAction("MyMeals");
        }



        //[HttpGet]
        //public IActionResult Create()
        //{

        //    List<MealsDetailsViewModel> mealsDetailsViewModel = new List<MealsDetailsViewModel>();
        //    List<Produkt> produkty = _dataMealsService.GetProducts().ToList();
        //    var resault = Tuple.Create(produkty, mealsDetailsViewModel);
        //    return View(resault);
        //}
        //[HttpPost]
        //public IActionResult Create(List<MealsDetailsViewModel> mealsDetailsViewModel, [FromForm] string nazwa) {
        //    //foreach (var item in daniaProdukty) {
        //    //    if (daniaprodukt.IdDania == null || daniaprodukt.IdProduktu == null) {
        //    //        danie.DaniaProdukty.Remove(daniaprodukt);
        //    //    }
        //    //}


        //    //_db.Add(danie);
        //    //_db.SaveChanges();
        //    return RedirectToAction("MyMeals");

        //}

        [HttpGet]
        [Route("MealsController/GetProductValues/{id}")]
        public ActionResult<List<decimal>> GetProductValues([FromBody] int id)
        {

            // Logika do pobrania listy decimal na podstawie id
            List<decimal> resultList = GetProductsDetails(id);
            return resultList == null ? NotFound() : Ok(resultList); ;
        }
        [HttpPost]
        //[Route("MealsController/GetProductValues2")]
        public ActionResult<int> GetProductValues2()
        {
            return 5;
        }

        private List<decimal> GetProductsDetails(int id)
        {

            Produkt szukanyProdukt = _dataMealsService.GetProducts().ToList().Find(produkt => produkt.Id == id);
            List<decimal> detale = new List<decimal>
            {
                (decimal)szukanyProdukt.Kaloryczność,
                (decimal)szukanyProdukt.Białko,
                (decimal)szukanyProdukt.Tłuszcz,
                (decimal)szukanyProdukt.Węglowodany,
                (decimal)szukanyProdukt.Błonnik,

            };

            return detale;

        }

        public IActionResult Create()
        {

            ViewBag.ProduktyLista = GetProductsSelect();
            List<Produkt> produkt1 = _dataMealsService.GetProducts().ToList();
            //var modifiedList = produkt1.Select(p => new { Id = p.Id, Kaloryczność = p.Kaloryczność }).ToList();
            ViewBag.Kalorycznosc = produkt1.Select(p => new { Id = p.Id, Kaloryczność = p.Kaloryczność }).ToList();
            ViewBag.Bialko = produkt1.Select(p => new { Id = p.Id, Bialko = p.Białko }).ToList();
            ViewBag.Tluszcz = produkt1.Select(p => new { Id = p.Id, Tluszcz = p.Tłuszcz }).ToList();
            ViewBag.Weglowodany = produkt1.Select(p => new { Id = p.Id, Weglowodany = p.Węglowodany }).ToList();
            ViewBag.Blonnik = produkt1.Select(p => new { Id = p.Id, Blonnik = p.Błonnik }).ToList();
            List<Produkt> produkt = _dataMealsService.GetProducts().ToList();
            Produkt nowy = produkt.FirstOrDefault();
            var model = new DanieViewModel();
            model.Produkty.Add(new Produkt() { Id = 1 });
            //var model = new DanieViewModel
            //{
            //    Produkty = posortowanaLista,
            //    IdProduktu = new List<int>() { 1, 2, 3 }
            //};

            return View(model);
        }

        //private dynamic GetKalorycznosc()
        //{
        //    List<Produkt> produkt = _dataMealsService.GetProducts().ToList();
        //}

        private List<SelectListItem> GetProductsSelect()
        {
            var lstProducts = new List<SelectListItem>();
            List<Produkt> produkt = _dataMealsService.GetProducts().ToList();
            List<Produkt> products = produkt.OrderBy(p => p.Nazwa).ToList();

            lstProducts = products.Select(ut => new SelectListItem()
            {
                Value = ut.Id.ToString(),
                Text = ut.Nazwa.ToString()
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "Wybierz Produkt"
            };

            lstProducts.Insert(0, defItem);

            return lstProducts;
        }

        [HttpPost]
        public IActionResult Create(DanieViewModel model)
        {
            if (ModelState.IsValid)
            {
                //tutaj logika zapisu do bazy danych

                return RedirectToAction("Meals", "MyMeals");
            }

            // Jeśli wystąpił błąd, ponowne wyrenderowanie widoku z modelem
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Dania danie = _db.Dania.Where(p => p.Id == Id).FirstOrDefault();


            return View(danie);
            //Dania danie = _db.Dania.Where(p => p.Id == Id).FirstOrDefault();
            //return View(danie);
        }
        [HttpPost]
        public IActionResult Edit(DaniaProdukty danie)
        {
            _db.Attach(danie);
            _db.Entry(danie).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("MyMeals");

        }

    }
}
