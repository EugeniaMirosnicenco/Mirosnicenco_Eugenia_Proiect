namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class PredictionHistory
    {
        public int Id { get; set; }

        public string Country { get; set; } = string.Empty;
        public int Household_Size { get; set; }
        public string Income_Level { get; set; } = string.Empty;
        public string Urban_Rural { get; set; } = string.Empty;
        public int Adoption_Year { get; set; }

        public double Monthly_Usage_kWh { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
