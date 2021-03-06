﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace ShotgunAdapters.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
                  : base(ConfigurationManager.AppSettings["DbConnection"], throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Caliber>().ToTable("Calibers")
                .Property(c => c.Diameter).HasPrecision(18, 3);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<Cstieg.Sales.Models.Order>().ToTable("Orders");
            modelBuilder.Entity<Cstieg.Sales.Models.OrderDetail>().ToTable("OrderDetails");
            modelBuilder.Entity<Cstieg.Sales.Models.Customer>().ToTable("Customers");
            modelBuilder.Entity<Cstieg.Sales.Models.ShipToAddress>().ToTable("Addresses");
            modelBuilder.Entity<Cstieg.Sales.Models.WebImage>().ToTable("WebImages");
        }


        public DbSet<Caliber> Calibers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cstieg.Sales.Models.OrderDetail> OrderDetails { get; set; }
        public DbSet<Cstieg.Sales.Models.Customer> Customers { get; set; }
        public DbSet<Cstieg.Sales.Models.ShipToAddress> Addresses { get; set; }
        public DbSet<Cstieg.Sales.Models.Order> Orders { get; set; }
        public DbSet<Cstieg.Sales.Models.WebImage> WebImages { get; set; }


    }
}