using CarProjectIIS.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarProjectIIS.Data
{
    public class CarContext : DbContext
    {
        public CarContext
            (
            DbContextOptions<CarContext> options
            ) : base(options){ }
        public DbSet<CarItem> CarItems { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
    }
}
