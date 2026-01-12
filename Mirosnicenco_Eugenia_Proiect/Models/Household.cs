using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class Household
    {
        public int HouseholdId { get; set; }
        public int CountryId { get; set; }  // FK

        [Range(1, 50)]
        public int HouseholdSize { get; set; }
        public string IncomeLevel { get; set; } // Low, Medium, High
        public string UrbanRural { get; set; } // Urban / Rural

        public Country? Country { get; set; }
        public ICollection<RenewableSystem>? RenewableSystems { get; set; }
        public ICollection<EnergyUsage>? EnergyUsages { get; set; }
    }
}
