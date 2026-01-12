using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        [Range(0, double.MaxValue)]
        public decimal AvgIncome { get; set; }

        public ICollection<Household>? Households { get; set; }
    }
}
