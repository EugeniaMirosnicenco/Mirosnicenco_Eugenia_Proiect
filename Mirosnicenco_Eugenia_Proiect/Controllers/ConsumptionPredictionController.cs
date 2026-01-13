using Microsoft.AspNetCore.Mvc;
using Mirosnicenco_Eugenia_Proiect.Models;
using Mirosnicenco_Eugenia_Proiect.Services;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Controllers
{
    public class ConsumptionPredictionController : Controller
    {
        private readonly IConsumptionPredictionService _consumptionService;

        public ConsumptionPredictionController(IConsumptionPredictionService consumptionService)
        {
            _consumptionService = consumptionService;
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

            return View(model);
        }


    }
}
