using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetAssesment.Models;

namespace DotNetAssesment.Data
{
    public class DotNetAssesmentContext : DbContext
    {
        public DotNetAssesmentContext (DbContextOptions<DotNetAssesmentContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owner { get; set; }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Claim> Claim { get; set; }
    }
}
