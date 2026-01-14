using DataAccess = Mirosnicenco_Eugenia_Proiect.Data;
using ModelAccess = Mirosnicenco_Eugenia_Proiect.Models;
using Grpc.Core;
using GrpcPredictionHistoryService;

namespace GrpcPredictionHistoryService.Services
{
    public class GrpcCRUDService : PredictionHistoryService.PredictionHistoryServiceBase
    {
        private DataAccess.LibraryContext db = null;
        public GrpcCRUDService(DataAccess.LibraryContext db)
        {
            this.db = db;
        }

        public override Task<PredictionHistoryList> GetAll(Empty empty, ServerCallContext context)
        {
            var list = new PredictionHistoryList();

            var query = from ph in db.PredictionHistory
                        select new PredictionHistory
                        {
                            Id = ph.Id,
                            Country = ph.Country,
                            HouseholdSize = ph.Household_Size,
                            IncomeLevel = ph.Income_Level,
                            UrbanRural = ph.Urban_Rural,
                            AdoptionYear = ph.Adoption_Year,
                            MonthlyUsageKWh = ph.Monthly_Usage_kWh,
                            CreatedAt = ph.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                        };

            list.Item.AddRange(query.ToArray());

            return Task.FromResult(list);
        }

        public override Task<PredictionHistory> Get(PredictionHistoryId requestData, ServerCallContext context)
        {
            PredictionHistory phMessage = null;
            var ph = db.PredictionHistory.Find(requestData.Id);

            if (ph != null)
            {
                phMessage = new PredictionHistory
                {
                    Id = ph.Id,
                    Country = ph.Country,
                    HouseholdSize = ph.Household_Size,
                    IncomeLevel = ph.Income_Level,
                    UrbanRural = ph.Urban_Rural,
                    AdoptionYear = ph.Adoption_Year,
                    MonthlyUsageKWh = ph.Monthly_Usage_kWh,
                    CreatedAt = ph.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
            else
            {
                phMessage = new PredictionHistory();
            }

            return Task.FromResult(phMessage);
        }

        public override Task<Empty> Delete(PredictionHistoryId requestData, ServerCallContext context)
        {
            var ph = db.PredictionHistory.Find(requestData.Id);

            if (ph != null)
            {
                db.PredictionHistory.Remove(ph);
                db.SaveChanges();
            }

            return Task.FromResult(new Empty());
        }

    }
}
