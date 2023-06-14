using projekt.Models;
using System.Diagnostics;

namespace projekt.Services
{
    public interface IDataMealsService
    {
        IEnumerable<Dania> GetMeals();
        IEnumerable<Produkt> GetProducts();
    }

    //service do odczytu produktow zaraz po starcie aplikacji
    public class DataMealsService : IDataMealsService
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<Produkt> _productsList;
        private IEnumerable<Dania> _mealsList;

        public DataMealsService(ApplicationDbContext context)
        {
            _context = context;
            _productsList = null;
            _mealsList = null;
            Debug.WriteLine("Jestem konstruktor");
        }

        public IEnumerable<Dania> GetMeals()
        {
            if (_mealsList == null)
            {
                _mealsList = _context.Dania.ToList();
            }
            return _mealsList;
        }

        public IEnumerable<Produkt> GetProducts()
        {
            if (_productsList == null)
            {
                _productsList = _context.Produkty.ToList();
            }
            return _productsList;

        }

    }
}
