using projekt.Models;
using System.Diagnostics;

namespace projekt.Services
{
    public interface IDataMealsService
    {
        IEnumerable<Dania> GetMeals();
        IEnumerable<Produkt> GetProducts();
        List<decimal> GetJadlospis(int id);
        IEnumerable<Produkt> GetMealsDetails(int id);
        String GetMealName(int id);
        int DeleteMeal(int id);
    }

    //service do odczytu produktow zaraz po starcie aplikacji
    public class DataMealsService : IDataMealsService
    {
        private readonly ApplicationDbContext _context;
        private IEnumerable<Produkt> _productsList;
        private IEnumerable<Produkt> _mealsDetails;
        private IEnumerable<Dania> _mealsList;
        

        public DataMealsService(ApplicationDbContext context)
        {
            _context = context;
            _productsList = null;
            _mealsList = null;
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

        //public IEnumerable<Produkt> GetMealsDetails(int id) {
        //    IEnumerable<DaniaProdukty> _daniaProdukty;
        //    foreach (DaniaProdukty item in _context.DaniaProdukty.ToList()) { 
        //        if (item.IdDania == id)
        //            _daniaProdukty.Append(_productsList.Where(item.IdProduktu));
        //    }

        //    return _daniaProdukty;
        //}

        public IEnumerable<Produkt> GetMealsDetails(int id)
        {
            IEnumerable<DaniaProdukty> _daniaProdukty = _context.DaniaProdukty.ToList();
            List<Produkt> _productsList = _context.Produkty.ToList();
            List<Produkt> mealsDetails = new List<Produkt>();

            foreach (DaniaProdukty item in _daniaProdukty)
            {
                if (item.IdDania == id)
                {
                    Produkt meal = _productsList.FirstOrDefault(p => p.Id == item.IdProduktu);
                    if (meal != null)
                    {
                        mealsDetails.Add(meal);
                    }
                }
            }

            return mealsDetails;
        }

        public int DeleteMeal(int id)
        {
            IEnumerable<DaniaProdukty> _daniaProdukty = _context.DaniaProdukty.ToList();
            IEnumerable<Jadlospis> _jadlospis = _context.Jadlospis.ToList();
            var meal = _context.Dania.Find(id); 
            foreach (var item in _daniaProdukty) {
                if (item.IdDania == id) { 
                    _context.DaniaProdukty.Remove(item);
                }
            }
            foreach (var item in _jadlospis)
            {
                if (item.IdDania == id)
                {
                    _context.Jadlospis.Remove(item);
                }
            }

            _context.Dania.Remove(meal);
            _context.SaveChanges();
            return id;
        }

        public List<decimal> GetJadlospis(int id)
        {
            IEnumerable<DaniaProdukty> _dania_produkty = _context.DaniaProdukty.ToList();
            List<decimal> ilosc = new List<decimal>();
            foreach (var item in _dania_produkty) {
                if (item.IdDania == id) {
                    ilosc.Add(item.Ilość);
                }
            }
            return ilosc;
        }

        string IDataMealsService.GetMealName(int id)
        {

            IEnumerable<Dania> dania = _context.Dania.ToList();
            foreach (var item in dania)
            {
                if (item.Id == id) {
                    return item.NazwaDania;
                }
                
            }
            return string.Empty;
        }
    }
}
