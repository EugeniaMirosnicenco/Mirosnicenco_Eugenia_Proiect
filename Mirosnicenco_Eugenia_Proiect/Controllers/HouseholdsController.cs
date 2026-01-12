using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mirosnicenco_Eugenia_Proiect.Data;
using Mirosnicenco_Eugenia_Proiect.Models;

namespace Mirosnicenco_Eugenia_Proiect.Controllers
{
    public class HouseholdsController : Controller
    {
        private readonly LibraryContext _context;

        public HouseholdsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Households
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["HouseholdSizeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "size_desc" : "";
            ViewData["IncomeLevelSortParm"] = sortOrder == "IncomeLevel" ? "income_desc" : "IncomeLevel";
            ViewData["UrbanRuralSortParm"] = sortOrder == "UrbanRural" ? "urban_desc" : "UrbanRural";
            ViewData["CountrySortParm"] = sortOrder == "Country" ? "country_desc" : "Country";

            var libraryContext = from h in _context.Household.Include(h => h.Country)
                                 select h;

            if (!String.IsNullOrEmpty(searchString))
            {
                libraryContext = libraryContext.Where(h => h.Country.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "size_desc":
                    libraryContext = libraryContext.OrderByDescending(h => h.HouseholdSize);
                    break;
                case "IncomeLevel":
                    libraryContext = libraryContext.OrderBy(h => h.IncomeLevel);
                    break;
                case "income_desc":
                    libraryContext = libraryContext.OrderByDescending(h => h.IncomeLevel);
                    break;
                case "UrbanRural":
                    libraryContext = libraryContext.OrderBy(h => h.UrbanRural);
                    break;
                case "urban_desc":
                    libraryContext = libraryContext.OrderByDescending(h => h.UrbanRural);
                    break;
                case "Country":
                    libraryContext = libraryContext.OrderBy(h => h.Country.Name);
                    break;
                case "country_desc":
                    libraryContext = libraryContext.OrderByDescending(h => h.Country.Name);
                    break;
                default:
                    libraryContext = libraryContext.OrderBy(h => h.HouseholdSize); 
                    break;
            }

            return View(await libraryContext.AsNoTracking().ToListAsync());
        }

        // GET: Households/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Household
                .Include(h => h.Country)
                .Include(h => h.EnergyUsages) 
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.HouseholdId == id);

            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // GET: Households/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "CountryId", "Name");

            ViewData["IncomeLevel"] = new SelectList(new[]
            {
                "Low",
                "Medium",
                "High"
            });

            ViewData["UrbanRural"] = new SelectList(new[]
            {
                "Urban",
                "Rural"
            });

            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseholdId,CountryId,HouseholdSize,IncomeLevel,UrbanRural")] Household household)
        {
            if (ModelState.IsValid)
            {
                _context.Add(household);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "CountryId", "Name", household.CountryId);

            ViewData["IncomeLevel"] = new SelectList(
                new[] { "Low", "Medium", "High" },
                household.IncomeLevel
            );

            ViewData["UrbanRural"] = new SelectList(
                new[] { "Urban", "Rural" },
                household.UrbanRural
            );

            return View(household);
        }

        // GET: Households/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Household.FindAsync(id);
            if (household == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "CountryId", "Name", household.CountryId);

            ViewData["IncomeLevel"] = new SelectList(
                new[] { "Low", "Medium", "High" },
                household.IncomeLevel
            );

            ViewData["UrbanRural"] = new SelectList(
                new[] { "Urban", "Rural" },
                household.UrbanRural
            );

            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseholdId,CountryId,HouseholdSize,IncomeLevel,UrbanRural")] Household household)
        {
            if (id != household.HouseholdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(household);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdExists(household.HouseholdId))
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
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "CountryId", "Name", household.CountryId);

            ViewData["IncomeLevel"] = new SelectList(
                new[] { "Low", "Medium", "High" },
                household.IncomeLevel
            );

            ViewData["UrbanRural"] = new SelectList(
                new[] { "Urban", "Rural" },
                household.UrbanRural
            );

            return View(household);
        }

        // GET: Households/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Household
                .Include(h => h.Country)
                .FirstOrDefaultAsync(m => m.HouseholdId == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var household = await _context.Household.FindAsync(id);
            if (household != null)
            {
                _context.Household.Remove(household);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseholdExists(int id)
        {
            return _context.Household.Any(e => e.HouseholdId == id);
        }
    }
}
