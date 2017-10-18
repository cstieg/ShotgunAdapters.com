using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Helpers;

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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Caliber>().ToTable("Calibers");
            modelBuilder.Entity<Cstieg.Image.WebImage>().ToTable("WebImages");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Cstieg.ShoppingCart.Order>().ToTable("Orders");
            modelBuilder.Entity<Cstieg.ShoppingCart.OrderDetail>().ToTable("OrderDetails");
            modelBuilder.Entity<Cstieg.ShoppingCart.Customer>().ToTable("Customers");
            modelBuilder.Entity<Cstieg.ShoppingCart.ShipToAddress>().ToTable("Addresses");
        }


        public DbSet<Caliber> Calibers { get; set; }
        public DbSet<Cstieg.Image.WebImage> WebImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cstieg.ShoppingCart.OrderDetail> OrderDetails { get; set; }
        public DbSet<Cstieg.ShoppingCart.Customer> Customers { get; set; }
        public DbSet<Cstieg.ShoppingCart.ShipToAddress> Addresses { get; set; }
        public DbSet<Cstieg.ShoppingCart.Order> Orders { get; set; }


    }
}