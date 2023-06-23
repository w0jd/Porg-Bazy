using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using projekt.Models;

namespace projekt.Controllers
{
    public class ProduktsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produkts
        public ActionResult Index(int pg=1)
        {
            List<Produkt> produkty = _context.Produkty.ToList();
            const int pageSize = 25;
            if (pg < 1)
                pg = 1;
            int prodCount = produkty.Count();
            var pager = new Pager(prodCount,pg, pageSize);
            int prodSkip = (pg - 1) * pageSize;
            var data = produkty.Skip(prodSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager=pager;
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
        
        // GET: Produkts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produkty == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // GET: Produkts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Kaloryczność,Białko,Tłuszcz,Węglowodany,Błonnik")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produkty == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produkt);
        }

        // POST: Produkts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Kaloryczność,Białko,Tłuszcz,Węglowodany,Błonnik")] Produkt produkt)
        {
            if (id != produkt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produkty == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produkty == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produkty'  is null.");
            }
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkty.Remove(produkt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
          return (_context.Produkty?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
