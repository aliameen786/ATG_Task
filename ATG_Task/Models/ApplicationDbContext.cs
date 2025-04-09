using Microsoft.EntityFrameworkCore;

namespace ATG_Task.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<OrderModel> Products { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<UsersModel> Users { get; set; }
    }
}
