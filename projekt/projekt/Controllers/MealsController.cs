using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using projekt.Services;

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

        public IActionResult Products()
        {
            var products = _dataMealsService.GetProducts();
            return View(products);
        }

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
