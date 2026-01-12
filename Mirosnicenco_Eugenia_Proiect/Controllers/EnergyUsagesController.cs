using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mirosnicenco_Eugenia_Proiect.Data;
using Mirosnicenco_Eugenia_Proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Controllers
{
    public class EnergyUsagesController : Controller
    {
        private readonly LibraryContext _context;

        public EnergyUsagesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: EnergyUsages
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            
            ViewData["YearSortParm"] = String.IsNullOrEmpty(sortOrder) ? "year_desc" : "";
            ViewData["ConsumptionSortParm"] = sortOrder == "Consumption" ? "consumption_desc" : "Consumption";
            ViewData["HouseholdSortParm"] = sortOrder == "Household" ? "household_desc" : "Household";

            var usages = _context.EnergyUsage
                .Include(e => e.Household)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                usages = usages.Where(e => e.Year.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "year_desc":
                    usages = usages.OrderByDescending(e => e.Year);
                    break;
                case "Consumption":
                    usages = usages.OrderBy(e => e.AvgMonthlyConsumption);
                    break;
                case "consumption_desc":
                    usages = usages.OrderByDescending(e => e.AvgMonthlyConsumption);
                    break;
                case "Household":
                    usages = usages.OrderBy(e => e.HouseholdId);
                    break;
                case "household_desc":
                    usages = usages.OrderByDescending(e => e.HouseholdId);
                    break;
                default:
                    usages = usages.OrderBy(e => e.Year);
                    break;
            }

            return View(await usages.AsNoTracking().ToListAsync());
        }

        // GET: EnergyUsages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyUsage = await _context.EnergyUsage
                .Include(e => e.Household)
                .FirstOrDefaultAsync(m => m.EnergyUsageId == id);
            if (energyUsage == null)
            {
                return NotFound();
            }

            return View(energyUsage);
        }

        // GET: EnergyUsages/Create
        public IActionResult Create()
        {
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId");
            return View();
        }

        // POST: EnergyUsages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnergyUsageId,HouseholdId,Year,AvgMonthlyConsumption")] EnergyUsage energyUsage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(energyUsage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId", energyUsage.HouseholdId);
            return View(energyUsage);
        }

        // GET: EnergyUsages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyUsage = await _context.EnergyUsage.FindAsync(id);
            if (energyUsage == null)
            {
                return NotFound();
            }
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId", energyUsage.HouseholdId);
            return View(energyUsage);
        }

        // POST: EnergyUsages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnergyUsageId,HouseholdId,Year,AvgMonthlyConsumption")] EnergyUsage energyUsage)
        {
            if (id != energyUsage.EnergyUsageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(energyUsage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnergyUsageExists(energyUsage.EnergyUsageId))
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
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId", energyUsage.HouseholdId);
            return View(energyUsage);
        }

        // GET: EnergyUsages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyUsage = await _context.EnergyUsage
                .Include(e => e.Household)
                .FirstOrDefaultAsync(m => m.EnergyUsageId == id);
            if (energyUsage == null)
            {
                return NotFound();
            }

            return View(energyUsage);
        }

        // POST: EnergyUsages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var energyUsage = await _context.EnergyUsage.FindAsync(id);
            if (energyUsage != null)
            {
                _context.EnergyUsage.Remove(energyUsage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnergyUsageExists(int id)
        {
            return _context.EnergyUsage.Any(e => e.EnergyUsageId == id);
        }
    }
}
