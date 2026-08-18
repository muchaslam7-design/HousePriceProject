using HousePriceApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HousePriceApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PredictionRecord> PredictionRecords => Set<PredictionRecord>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Fix decimal precision for SQL Server
            builder.Entity<PredictionRecord>()
                .Property(p => p.PredictedPrice)
                .HasPrecision(18, 2);
        }
    }
}