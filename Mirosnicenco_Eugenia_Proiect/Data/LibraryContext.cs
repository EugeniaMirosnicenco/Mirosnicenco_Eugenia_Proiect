using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mirosnicenco_Eugenia_Proiect.Models;

namespace Mirosnicenco_Eugenia_Proiect.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Mirosnicenco_Eugenia_Proiect.Models.Household> Household { get; set; } = default!;
        public DbSet<Mirosnicenco_Eugenia_Proiect.Models.EnergyProvider> EnergyProvider { get; set; } = default!;
        public DbSet<Mirosnicenco_Eugenia_Proiect.Models.RenewableSystem> RenewableSystem { get; set; } = default!;
        public DbSet<Mirosnicenco_Eugenia_Proiect.Models.EnergyUsage> EnergyUsage { get; set; } = default!;
        public DbSet<Mirosnicenco_Eugenia_Proiect.Models.Country> Country { get; set; } = default!;
    }
}
