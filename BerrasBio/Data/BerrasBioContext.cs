#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BerrasBio.Data
{
    public class BerrasBioContext : IdentityDbContext<User>
    {
        public BerrasBioContext (DbContextOptions<BerrasBioContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<Bookable_Seats>(b =>
            {
                b.HasIndex(e => new { e.SeatID, e.ShowID }).IsUnique();
            });
        }


        public DbSet<BerrasBio.Models.Movie> Movie { get; set; }
        public DbSet<BerrasBio.Models.Saloon> Saloon { get; set; }
        public DbSet<BerrasBio.Models.Show> Show { get; set; }
        public DbSet<BerrasBio.Models.Booking> Booking { get; set; }
        public DbSet<BerrasBio.Models.Bookable_Seats> Bookable_Seats { get; set; }
    }
}
