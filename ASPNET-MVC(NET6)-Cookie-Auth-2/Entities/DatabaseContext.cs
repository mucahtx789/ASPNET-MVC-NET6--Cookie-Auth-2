
using Microsoft.EntityFrameworkCore;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<uretim> Uretims { get; set; }
       
        public DbSet<bilgiislem> Bilgiislems { get; set; }
        public DbSet<insankaynaklari> Insankaynaklaris { get; set; }
    }
}