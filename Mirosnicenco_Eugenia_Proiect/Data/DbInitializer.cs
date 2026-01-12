using Microsoft.EntityFrameworkCore;
using Mirosnicenco_Eugenia_Proiect.Models;

namespace Mirosnicenco_Eugenia_Proiect.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {

                if (context.Household.Any())
                {
                    return;
                }

                var countries = new Country[]
                {
                    new Country{Name="Romania", Region="Europe", AvgIncome=35000},
                    new Country{Name="USA", Region="North America", AvgIncome=50000}
                };
                context.Country.AddRange(countries);
                context.SaveChanges();

                var households = new Household[]
{
                    new Household{CountryId=countries[0].CountryId, HouseholdSize=4, IncomeLevel="Medium", UrbanRural="Urban"},
                    new Household{CountryId=countries[1].CountryId, HouseholdSize=3, IncomeLevel="High", UrbanRural="Urban"}
};
                context.Household.AddRange(households);
                context.SaveChanges();

                var providers = new EnergyProvider[]
                {
                    new EnergyProvider{ProviderName="GreenEnergy Ltd", ProviderType="Private", GreenEnergyShare=0.9},
                    new EnergyProvider{ProviderName="EcoPower", ProviderType="Public", GreenEnergyShare=0.75}
                };
                context.EnergyProvider.AddRange(providers);
                context.SaveChanges();

                var systems = new RenewableSystem[]
                {
                    new RenewableSystem{HouseholdId=households[0].HouseholdId, EnergyProviderId=providers[0].EnergyProviderId, SystemType="Solar", CapacityKW=5.0, InstalationYear=2021},
                    new RenewableSystem{HouseholdId=households[1].HouseholdId, EnergyProviderId=providers[1].EnergyProviderId, SystemType="Wind", CapacityKW=2.5, InstalationYear=2022}
                };
                context.RenewableSystem.AddRange(systems);
                context.SaveChanges();

                var usages = new EnergyUsage[]
                {
                    new EnergyUsage{HouseholdId=households[0].HouseholdId, Year=2023, AvgMonthlyConsumption=350},
                    new EnergyUsage{HouseholdId=households[1].HouseholdId, Year=2023, AvgMonthlyConsumption=420}
                };
                context.EnergyUsage.AddRange(usages);

                context.SaveChanges();
            }
        }
    }

}
