using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace WebApplication7.Models
{
    public class DepiContext : IdentityDbContext<ApplicationUser>
    {
        public DepiContext()
        {
        }

        public DepiContext(DbContextOptions<DepiContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Booking> Booking { get; set; }
        
        public DbSet<Place> Places { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<WishList> wishLists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }


    }
}

