using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class DepiContext : IdentityDbContext<ApplicationUser>
    {
        public DepiContext() { }

        public DepiContext(DbContextOptions<DepiContext> options) : base(options) { }
        
        // Entity sets
        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Place entity configuration
            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(e => e.Place_Id);
                entity.Property(e => e.Place_Name).IsRequired();
                entity.Property(e => e.Place_Type).IsRequired();
                entity.Property(e => e.Place_City);
                entity.Property(e => e.Place_Price).IsRequired();
                entity.Property(e => e.Place_Rating).HasDefaultValue("unrated");
                entity.Property(e => e.cnt).HasDefaultValue(0);
                entity.Property(e => e.SumOfRates).HasDefaultValue(0);
                entity.Property(e => e.Description);
                entity.Ignore(e => e.clientFile);
                entity.Ignore(e => e.imageSrc);
            });

            // Review entity configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Review_Id);
                entity.Property(e => e.Comment);
                entity.Property(e => e.UserName);
                entity.Property(e => e.Date);
                entity.Property(e => e.Rating).IsRequired();
                entity.HasOne(e => e.user)
                    .WithMany()
                    .HasForeignKey(e => e.User_ID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.place)
                    .WithMany()
                    .HasForeignKey(e => e.Place_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Booking entity configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.booking_Id);
                entity.Property(e => e.promotion_Code);
                entity.Property(e => e.payment_state);
                entity.Property(e => e.total_amount);
                entity.Property(e => e.total_Dayes);
                entity.Property(e => e.Place_ID).IsRequired();
                entity.HasOne(e => e.user)
                    .WithMany()
                    .HasForeignKey(e => e.User_ID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.place)
                    .WithMany()
                    .HasForeignKey(e => e.Place_ID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // WishList entity configuration
            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.place)
                    .WithMany()
                    .HasForeignKey(e => e.PlaceId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.user)
                    .WithMany()
                    .HasForeignKey(e => e.UserID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Payment entity configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Payment_Id);
                entity.Property(e => e.Amount).IsRequired();
                entity.HasOne(e => e.booking)
                    .WithOne()
                    .HasForeignKey<Payment>(e => e.booking_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Promotion entity configuration
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.promotion_Id);
                entity.Property(e => e.promotion_Code).IsRequired();
                entity.Property(e => e.Discount_Amount).IsRequired();
                entity.Property(e => e.Discount_Amount).HasAnnotation("Range", new[] { 0, 100 });
            });

            // Admin entity configuration
            modelBuilder.Entity<Admin>(entity =>
            {
                // Admin inherits from ApplicationUser
                entity.Property(e => e.Salary);
                entity.Property(e => e.ImageUrl);
            });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuration is handled in Program.cs via dependency injection
        }
    }
} 