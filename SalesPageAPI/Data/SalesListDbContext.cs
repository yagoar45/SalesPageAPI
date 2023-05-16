using Microsoft.EntityFrameworkCore;
using SalesPageAPI.Models;

namespace SalesPageAPI.Data
{
    public class SalesListDbContext : DbContext
    {
        public SalesListDbContext(DbContextOptions<SalesListDbContext> opts) :base(opts)
        {}
        public virtual DbSet<SalesListModel> DbSalesList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
