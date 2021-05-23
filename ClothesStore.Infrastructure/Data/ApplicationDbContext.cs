using ClothesStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothesStore.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Clothes> Clothes { get; set; }
        public virtual DbSet<ClothesMark> ClothesMarks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<ClothesOrder> ClothesOrders { get; set; }
        public virtual DbSet<ClothesType> ClothesTypes { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClothesMark>().HasIndex(e => new { e.ClothesId, e.SizeId }).IsUnique();
            modelBuilder.Entity<ClothesOrder>().HasIndex(o => new { o.ClothesUnitId, o.OrderId }).IsUnique();
            modelBuilder.Entity<ClothesType>().HasIndex(p => new { p.Destinantion, p.Name }).IsUnique();
        }
    }
}
