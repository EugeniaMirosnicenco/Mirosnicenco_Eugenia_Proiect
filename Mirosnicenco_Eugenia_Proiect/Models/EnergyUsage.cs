using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class EnergyUsage
    {
        public int EnergyUsageId { get; set; }
        public int HouseholdId { get; set; } // FK

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Range(0, double.MaxValue)]
        public double AvgMonthlyConsumption { get; set; } // kWh

        public Household? Household { get; set; }
    }
}
