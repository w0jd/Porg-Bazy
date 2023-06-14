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
            var products = _dataMealsService.GetProducts();
            return View(products );
        }
    }
}
