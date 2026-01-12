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
    public class CountriesController : Controller
    {
        private readonly LibraryContext _context;

        public CountriesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RegionSortParm"] = sortOrder == "Region" ? "region_desc" : "Region";
            ViewData["IncomeSortParm"] = sortOrder == "AvgIncome" ? "income_desc" : "AvgIncome";

            var countries = from c in _context.Country
                            select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                countries = countries.Where(c => c.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    countries = countries.OrderByDescending(c => c.Name);
                    break;
                case "Region":
                    countries = countries.OrderBy(c => c.Region);
                    break;
                case "region_desc":
                    countries = countries.OrderByDescending(c => c.Region);
                    break;
                case "AvgIncome":
                    countries = countries.OrderBy(c => c.AvgIncome);
                    break;
                case "income_desc":
                    countries = countries.OrderByDescending(c => c.AvgIncome);
                    break;
                default:
                    countries = countries.OrderBy(c => c.Name);
                    break;
            }

            return View(await countries.AsNoTracking().ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .Include(c => c.Households)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CountryId == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewData["Region"] = new SelectList(new[]
            {
                "Europe",
                "North America",
                "South America",
                "Asia",
                "Africa",
                "Australia"
            });
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,Name,Region,AvgIncome")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Region"] = new SelectList(new[]
            {
                "Europe",
                "North America",
                "South America",
                "Asia",
                "Africa",
                "Australia"
            }, country.Region);

            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            ViewData["Region"] = new SelectList(new[]
{
                "Europe",
                "North America",
                "South America",
                "Asia",
                "Africa",
                "Australia"
            }, country.Region);

            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,Name,Region,AvgIncome")] Country country)
        {
            if (id != country.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryId))
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

            ViewData["Region"] = new SelectList(new[]
{
                "Europe",
                "North America",
                "South America",
                "Asia",
                "Africa",
                "Australia"
            }, country.Region);

            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country != null)
            {
                _context.Country.Remove(country);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.CountryId == id);
        }
    }
}
