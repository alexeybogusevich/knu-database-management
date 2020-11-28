using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;

namespace KNU.IT.DbManager.Connections
{
    public class AzureSqlDbContext : DbContext
    {
        public DbSet<Database> Databases { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Row> Rows { get; set; }

        public AzureSqlDbContext(DbContextOptions<AzureSqlDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Database
            modelBuilder.Entity<Database>().ToTable(nameof(Database));
            modelBuilder.Entity<Database>().HasKey(d => d.Id);
            modelBuilder.Entity<Database>().Property(t => t.Id).ValueGeneratedOnAdd();

            // Table
            modelBuilder.Entity<Table>().ToTable(nameof(Table));
            modelBuilder.Entity<Table>().HasKey(t => t.Id);
            modelBuilder.Entity<Table>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Table>().HasOne(t => t.Database)
                .WithMany().HasForeignKey(t => t.DatabaseId);
            modelBuilder.Entity<Table>().Property(t => t.Schema).HasColumnType("nvarchar(max)");

            // Table
            modelBuilder.Entity<Row>().ToTable(nameof(Row));
            modelBuilder.Entity<Row>().HasKey(t => t.Id);
            modelBuilder.Entity<Row>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Row>().HasOne(t => t.Table)
                .WithMany().HasForeignKey(t => t.TableId);
            modelBuilder.Entity<Row>().Property(t => t.Content).HasColumnType("nvarchar(max)");
        }
    }
}
