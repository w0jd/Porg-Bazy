using projekt.Models;
using System.Diagnostics;

namespace projekt.Services
{
    public interface IDataMealsService
    {
        IEnumerable<Produkt> GetProducts();
    }

    //service do odczytu produktow zaraz po starcie aplikacji
    public class DataMealsService : IDataMealsService
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<Produkt> _productsList;

        public DataMealsService(ApplicationDbContext context)
        {
            _context = context;
            _productsList = null;
            Debug.WriteLine("Jestem konstruktor");
        }

        public IEnumerable<Produkt> GetProducts()
        {
            if (_productsList == null)
            {
                Debug.WriteLine("Jestem tu");
                _productsList = _context.Produkty.ToList();
            }
            Debug.WriteLine("Jestem tu");
            return _productsList;

        }

    }
}
