using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class RenewableSystem
    {
        public int RenewableSystemId { get; set; }
        public int HouseholdId { get; set; } // FK
        public int EnergyProviderId { get; set; } // FK
        public string SystemType { get; set; } // Solar, Wind, Hybrid
        [Range(0.1, 10000)]
        public double CapacityKW { get; set; }
        [Range(1980, 2100)]
        public int InstalationYear { get; set; }

        public Household? Household { get; set; }
        public EnergyProvider? EnergyProvider { get; set; }
    }
}
