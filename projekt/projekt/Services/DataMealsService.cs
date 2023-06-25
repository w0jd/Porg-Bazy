﻿using projekt.Models;
using System.Diagnostics;

namespace projekt.Services
{
    public interface IDataMealsService
    {
        IEnumerable<Dania> GetMeals();
        IEnumerable<Produkt> GetProducts();
        IEnumerable<Produkt> GetMealsDetails(int id);
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
            var meal = _context.Dania.Find(id);
            _context.Dania.Remove(meal);
            _context.SaveChanges();
            return id;
        }
    }
}
