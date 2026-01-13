using System.Threading.Tasks;
using Mirosnicenco_Eugenia_Proiect.Models;

namespace Mirosnicenco_Eugenia_Proiect.Services
{
    public interface IConsumptionPredictionService
    {
        Task<double> PredictConsumptionAsync(ConsumptionInput input);
    }
}
