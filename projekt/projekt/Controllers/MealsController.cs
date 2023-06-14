using Microsoft.AspNetCore.Mvc;
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
    }
}
