using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using GrpcPredictionHistoryService;

namespace Mirosnicenco_Eugenia_Proiect.Controllers
{
    public class PredictionHistoryGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public PredictionHistoryGrpcController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:7215");
        }

        // GET: /PredictionHistoryGrpc/Index
        [HttpGet]
        public IActionResult Index()
        {
            var client = new PredictionHistoryService.PredictionHistoryServiceClient(channel);
            var history = client.GetAll(new Empty());

            return View(history);
        }

        // GET: /PredictionHistoryGrpc/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new PredictionHistoryService.PredictionHistoryServiceClient(channel);
            var prediction = client.Get(new PredictionHistoryId { Id = (int)id });

            if (prediction == null)
            {
                return NotFound();
            }

            return View(prediction);
        }

        // POST: /PredictionHistoryGrpc/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new PredictionHistoryService.PredictionHistoryServiceClient(channel);
            var response = client.Delete(new PredictionHistoryId { Id = id });

            return RedirectToAction(nameof(Index));
        }

    }
}
