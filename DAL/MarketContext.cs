using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DAL
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MarketWEB;Integrated Security=True;Connect Timeout=30");


        //}
        public DbSet<BrandDTO> Brands { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<ClientDTO> Clients { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<ProviderDTO> Providers { get; set; }
        public DbSet<SaleDTO> Sales { get; set; }
        public DbSet<ItemsSaleDTO> ItemSales { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
