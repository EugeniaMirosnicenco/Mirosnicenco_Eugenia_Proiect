using System.ComponentModel.DataAnnotations;

namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class ConsumptionPredictionViewModel
    {
        [Required(ErrorMessage = "Country is required.")]
        public string country { get; set; }

        [Range(1, 50, ErrorMessage = "Household size must be between 1 and 50.")]
        public int household_Size { get; set; }

        [Required(ErrorMessage = "Income level is required.")]
        public string income_Level { get; set; }

        [Required(ErrorMessage = "Urban/Rural selection is required.")]
        public string urban_Rural { get; set; }

        [Range(1980, 2100, ErrorMessage = "Adoption year must be between 1980 and 2100.")]
        public int adoption_Year { get; set; }
        public double monthly_Usage_kWh { get; set; }
    }
}
