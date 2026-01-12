using System.ComponentModel.DataAnnotations;

namespace Mirosnicenco_Eugenia_Proiect.Models
{
    public class EnergyProvider
    {
        public int EnergyProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderType { get; set; } // Public / Private

        [Range(0, 100, ErrorMessage = "Procentul de energie verde trebuie să fie între 0 și 100.")]
        public double GreenEnergyShare { get; set; } // %

        public ICollection<RenewableSystem>? RenewableSystems { get; set; }
    }
}
