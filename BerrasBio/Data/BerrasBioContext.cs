#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Models;

namespace BerrasBio.Data
{
    public class BerrasBioContext : DbContext
    {
        public BerrasBioContext (DbContextOptions<BerrasBioContext> options)
            : base(options)
        {
        }

        public DbSet<BerrasBio.Models.Account> Account { get; set; }
    }
}
