namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class DashboardViewModel
    {
        public int TotalPredictions { get; set; }
        public List<IncomeLevelStat> IncomeLevelStats { get; set; } = new();
        public List<ConsumptionBucketStat> ConsumptionBuckets { get; set; } = new();

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
