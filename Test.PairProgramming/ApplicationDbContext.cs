using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Test.PairProgramming.Models;

namespace Test.PairProgramming
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ProductName)
                      .IsRequired()
                      .HasMaxLength(100);

                //entity.Property(e => e.Price)
                //      .HasColumnType("decimal(18,2)");

                // Relationships
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Supplier)
                      .WithMany(s => s.Products)
                      .HasForeignKey(e => e.SupplierId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CategoryName)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // Configure Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.SupplierName)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}
