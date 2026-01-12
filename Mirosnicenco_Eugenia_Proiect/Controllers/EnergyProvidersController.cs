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
    public class EnergyProvidersController : Controller
    {
        private readonly LibraryContext _context;

        public EnergyProvidersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: EnergyProviders
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TypeSortParm"] = sortOrder == "ProviderType" ? "type_desc" : "ProviderType";
            ViewData["GreenSortParm"] = sortOrder == "GreenEnergyShare" ? "green_desc" : "GreenEnergyShare";

            var providers = from p in _context.EnergyProvider
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                providers = providers.Where(p => p.ProviderName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    providers = providers.OrderByDescending(p => p.ProviderName);
                    break;
                case "ProviderType":
                    providers = providers.OrderBy(p => p.ProviderType);
                    break;
                case "type_desc":
                    providers = providers.OrderByDescending(p => p.ProviderType);
                    break;
                case "GreenEnergyShare":
                    providers = providers.OrderBy(p => p.GreenEnergyShare);
                    break;
                case "green_desc":
                    providers = providers.OrderByDescending(p => p.GreenEnergyShare);
                    break;
                default:
                    providers = providers.OrderBy(p => p.ProviderName);
                    break;
            }

            return View(await providers.AsNoTracking().ToListAsync());
        }

        // GET: EnergyProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyProvider = await _context.EnergyProvider
                .Include(e => e.RenewableSystems)
                .ThenInclude(r => r.Household) 
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EnergyProviderId == id);

            if (energyProvider == null)
            {
                return NotFound();
            }

            return View(energyProvider);
        }

        // GET: EnergyProviders/Create
        public IActionResult Create()
        {
            ViewData["ProviderType"] = new SelectList(new[] {
                "Public",
                "Private"
            });

            return View();
        }

        // POST: EnergyProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnergyProviderId,ProviderName,ProviderType,GreenEnergyShare")] EnergyProvider energyProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(energyProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProviderType"] = new SelectList(new[] {
                "Public",
                "Private"
            }, energyProvider.ProviderType);

            return View(energyProvider);
        }

        // GET: EnergyProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyProvider = await _context.EnergyProvider.FindAsync(id);
            if (energyProvider == null)
            {
                return NotFound();
            }

            ViewData["ProviderType"] = new SelectList(new[] {
                "Public",
                "Private"
            }, energyProvider.ProviderType);

            return View(energyProvider);
        }

        // POST: EnergyProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnergyProviderId,ProviderName,ProviderType,GreenEnergyShare")] EnergyProvider energyProvider)
        {
            if (id != energyProvider.EnergyProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(energyProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnergyProviderExists(energyProvider.EnergyProviderId))
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

            ViewData["ProviderType"] = new SelectList(new[] {
                "Public",
                "Private"
            }, energyProvider.ProviderType);

            return View(energyProvider);
        }

        // GET: EnergyProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyProvider = await _context.EnergyProvider
                .FirstOrDefaultAsync(m => m.EnergyProviderId == id);
            if (energyProvider == null)
            {
                return NotFound();
            }

            return View(energyProvider);
        }

        // POST: EnergyProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var energyProvider = await _context.EnergyProvider.FindAsync(id);
            if (energyProvider != null)
            {
                _context.EnergyProvider.Remove(energyProvider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnergyProviderExists(int id)
        {
            return _context.EnergyProvider.Any(e => e.EnergyProviderId == id);
        }
    }
}
