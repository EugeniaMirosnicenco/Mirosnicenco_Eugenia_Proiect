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
    public class RenewableSystemsController : Controller
    {
        private readonly LibraryContext _context;

        public RenewableSystemsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: RenewableSystems
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["HouseholdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "household_desc" : "";
            ViewData["ProviderSortParm"] = sortOrder == "Provider" ? "provider_desc" : "Provider";
            ViewData["SystemTypeSortParm"] = sortOrder == "SystemType" ? "systemtype_desc" : "SystemType";
            ViewData["CapacitySortParm"] = sortOrder == "Capacity" ? "capacity_desc" : "Capacity";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";

            var systems = _context.RenewableSystem
                          .Include(r => r.Household)
                          .Include(r => r.EnergyProvider)
                          .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                systems = systems.Where(r =>
                    r.SystemType.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "household_desc":
                    systems = systems.OrderByDescending(r => r.HouseholdId);
                    break;
                case "Provider":
                    systems = systems.OrderBy(r => r.EnergyProvider.ProviderName);
                    break;
                case "provider_desc":
                    systems = systems.OrderByDescending(r => r.EnergyProvider.ProviderName);
                    break;
                case "SystemType":
                    systems = systems.OrderBy(r => r.SystemType);
                    break;
                case "systemtype_desc":
                    systems = systems.OrderByDescending(r => r.SystemType);
                    break;
                case "Capacity":
                    systems = systems.OrderBy(r => r.CapacityKW);
                    break;
                case "capacity_desc":
                    systems = systems.OrderByDescending(r => r.CapacityKW);
                    break;
                case "Year":
                    systems = systems.OrderBy(r => r.InstalationYear);
                    break;
                case "year_desc":
                    systems = systems.OrderByDescending(r => r.InstalationYear);
                    break;
                default:
                    systems = systems.OrderBy(r => r.RenewableSystemId);
                    break;
            }

            return View(await systems.AsNoTracking().ToListAsync());
        }

        // GET: RenewableSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renewableSystem = await _context.RenewableSystem
                .Include(r => r.Household)
                .Include(r => r.EnergyProvider)
                .FirstOrDefaultAsync(m => m.RenewableSystemId == id);

            if (renewableSystem == null)
            {
                return NotFound();
            }

            return View(renewableSystem);
        }

        // GET: RenewableSystems/Create
        public IActionResult Create()
        {
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId");
            ViewData["EnergyProviderId"] = new SelectList(_context.EnergyProvider, "EnergyProviderId", "ProviderName");
            ViewData["SystemType"] = new SelectList(new List<string> { "Solar", "Wind", "Hybrid" });
            return View();
        }

        // POST: RenewableSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RenewableSystemId,HouseholdId,EnergyProviderId,SystemType,CapacityKW,InstalationYear")] RenewableSystem renewableSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(renewableSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId", renewableSystem.HouseholdId);
            ViewData["EnergyProviderId"] = new SelectList(_context.EnergyProvider, "EnergyProviderId", "ProviderName", renewableSystem.EnergyProviderId);
            ViewData["SystemType"] = new SelectList(new List<string> { "Solar", "Wind", "Hybrid" }, renewableSystem.SystemType);
            return View(renewableSystem);
        }

        // GET: RenewableSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renewableSystem = await _context.RenewableSystem.FindAsync(id);
            if (renewableSystem == null)
            {
                return NotFound();
            }
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId", renewableSystem.HouseholdId);
            ViewData["EnergyProviderId"] = new SelectList(_context.EnergyProvider, "EnergyProviderId", "ProviderName", renewableSystem.EnergyProviderId);
            ViewData["SystemType"] = new SelectList(new List<string> { "Solar", "Wind", "Hybrid" }, renewableSystem.SystemType);
            return View(renewableSystem);
        }

        // POST: RenewableSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RenewableSystemId,HouseholdId,EnergyProviderId,SystemType,CapacityKW,InstalationYear")] RenewableSystem renewableSystem)
        {
            if (id != renewableSystem.RenewableSystemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(renewableSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RenewableSystemExists(renewableSystem.RenewableSystemId))
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
            ViewData["HouseholdId"] = new SelectList(_context.Household, "HouseholdId", "HouseholdId", renewableSystem.HouseholdId);
            ViewData["EnergyProviderId"] = new SelectList(_context.EnergyProvider, "EnergyProviderId", "ProviderName", renewableSystem.EnergyProviderId);
            ViewData["SystemType"] = new SelectList(new List<string> { "Solar", "Wind", "Hybrid" }, renewableSystem.SystemType);
            return View(renewableSystem);
        }

        // GET: RenewableSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renewableSystem = await _context.RenewableSystem
                .Include(r => r.Household)
                .Include(r => r.EnergyProvider)
                .FirstOrDefaultAsync(m => m.RenewableSystemId == id);
            if (renewableSystem == null)
            {
                return NotFound();
            }

            return View(renewableSystem);
        }

        // POST: RenewableSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var renewableSystem = await _context.RenewableSystem.FindAsync(id);
            if (renewableSystem != null)
            {
                _context.RenewableSystem.Remove(renewableSystem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RenewableSystemExists(int id)
        {
            return _context.RenewableSystem.Any(e => e.RenewableSystemId == id);
        }
    }
}
