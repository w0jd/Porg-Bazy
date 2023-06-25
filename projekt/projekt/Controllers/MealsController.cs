using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using projekt.Services;
using System.Collections.Generic;

namespace projekt.Controllers
{
    public class MealsController : Controller
    {

        private readonly IDataMealsService _dataMealsService;

        IEnumerable <Produkt> products;
  
        


        public MealsController(IDataMealsService DataProductService)
        {
            _dataMealsService = DataProductService;
             products = _dataMealsService.GetProducts();
             

        }

        public IActionResult Products()
        {
           // var products = _dataMealsService.GetProducts();
            return View(products);
        }

        public IActionResult MyMeals()
        {
            //var collection1 = GetCollection1Data();
            //var collection2 = GetCollection2Data();
            //var additionalData = GetAdditionalData();

            //var model = Tuple.Create(collection1, additionalData);

            //return View(model);


            //var products = _dataMealsService.GetProducts();
            var meals = _dataMealsService.GetMeals();

            var products_and_meals = Tuple.Create(products, meals);
            return View(products_and_meals);
        }
        [HttpPost]
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


        [HttpGet]
        public IActionResult Details(int id) // id elementu ktory chcemy pobra z bazy danych
        {
            var mealdetails = _dataMealsService.GetMealsDetails(id);
            return View(mealdetails);
        }

        [HttpPost]
        public IActionResult AddingMeals (Dania danie)
        {
            
            return View(danie);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dataMealsService.DeleteMeal(id);
            return RedirectToAction("MyMeals");
        }


    }
}
