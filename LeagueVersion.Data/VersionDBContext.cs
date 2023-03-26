using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueVersion.Data
{
    public class VersionDBContext : DbContext
    {
        public DbSet<Version> Versions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=VersionDB; Integrated Security=True; TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Version>(mb =>
            {
                mb.Property(version => version.Id);
                mb.Property(version => version.Name);

                mb.HasKey(version => version.Id);
            });
        }
    }
}
