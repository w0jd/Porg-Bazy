using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using projekt.Services;

namespace projekt.Controllers
{
    public class MealsController : Controller
    {

        private readonly IDataMealsService _dataMealsService;

    
        
        public MealsController(IDataMealsService DataProductService)
        {
            _dataMealsService = DataProductService;
      
          
        }
        public IActionResult Products(int pg = 1)
        {
            List<Produkt> produkty = _dataMealsService.GetProducts().ToList();
            const int pageSize = 25;
            if (pg < 1 )
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
            

            var products = _dataMealsService.GetProducts();
            var meals = _dataMealsService.GetMeals();

            var products_and_meals = Tuple.Create(products, meals);
            return View(products_and_meals );
        }

        public IActionResult GetItems(string searchTerm)
        {
            // Przykładowe dane
            var items = new List<string> { "Element 1", "Element 2", "Element 3" };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                items = items.Where(item => item.Contains(searchTerm)).ToList();
            }

            return Json(items);
        }


    }
}
