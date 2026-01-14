using Microsoft.AspNetCore.Mvc;
using Mirosnicenco_Eugenia_Proiect.Models;
using Mirosnicenco_Eugenia_Proiect.Services;
using Mirosnicenco_Eugenia_Proiect.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Controllers
{
    public class ConsumptionPredictionController : Controller
    {
        private readonly IConsumptionPredictionService _consumptionService;
        private readonly LibraryContext _context;

        public ConsumptionPredictionController(IConsumptionPredictionService consumptionService,
            LibraryContext context)
        {
            _consumptionService = consumptionService;
            _context = context;
        }

        // GET: /ConsumptionPrediction/Index 
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ConsumptionPredictionViewModel();
            return View(model);
        }

        // POST: /ConsumptionPrediction/Index 
        [HttpPost]
        public async Task<IActionResult> Index(ConsumptionPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var input = new ConsumptionInput
            {
                country = model.country,
                household_Size = model.household_Size,
                income_Level = model.income_Level,
                urban_Rural = model.urban_Rural,
                adoption_Year = model.adoption_Year
            };

            var prediction = await _consumptionService.PredictConsumptionAsync(input);

            model.monthly_Usage_kWh = prediction;

            var history = new PredictionHistory
            {
                Country = model.country,
                Household_Size = model.household_Size,
                Income_Level = model.income_Level,
                Urban_Rural = model.urban_Rural,
                Adoption_Year = model.adoption_Year,
                Monthly_Usage_kWh = prediction,
                CreatedAt = DateTime.Now
            };

            _context.PredictionHistory.Add(history);
            await _context.SaveChangesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var history = await _context.PredictionHistory
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(history);
        }

    }
}
