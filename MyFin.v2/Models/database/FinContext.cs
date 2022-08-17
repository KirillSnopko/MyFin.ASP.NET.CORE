using Microsoft.EntityFrameworkCore;
using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.Entities.database
{
    public class FinContext : DbContext
    {
        public DbSet<Credit> credits { get; set; }
        public DbSet<Depository> depositories { get; set; }
        public DbSet<Operation> operations { get; set; }

        public FinContext(DbContextOptions<FinContext> options)
            : base(options) => Database.EnsureCreated();

    }
}
