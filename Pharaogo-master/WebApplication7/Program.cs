using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;
using WebApplication7.Repositry;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.Helper;

namespace WebApplication7
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container
            builder.Services.AddControllersWithViews();
            
            // Configure database
            builder.Services.AddDbContext<DepiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DepiConnection")));

            // Configure identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
                options.Password.RequireDigit = true)
                .AddEntityFrameworkStores<DepiContext>();

            // Add AutoMapper - register the MappingProfile
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            // Register MapperHelper for easy use in controllers and repositories
            builder.Services.AddScoped<MapperHelper>();

            // Register repositories
            builder.Services.AddScoped<IPlace, Place_Repo>();
            builder.Services.AddScoped<IAdmin, Admin_Repo>();
            builder.Services.AddScoped<IBooking, Booking_Repo>();
            builder.Services.AddScoped<IPayment, Payment_Repo>();
            builder.Services.AddScoped<IReview, Review_Repo>();
            builder.Services.AddScoped<IUser, User_Repo>();
            builder.Services.AddScoped<IPromotion, Promotion_Repo>();
            builder.Services.AddScoped<IWishList, WishList_Repo>();
            builder.Services.AddScoped<ISearch, Search_Repo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            // Create default roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                
                // Create roles if they don't exist
                string[] roleNames = { "Admin", "Visitor" };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                
                // Create default admin if it doesn't exist
                var adminEmail = "admin@pharaogo.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    
                    var result = await userManager.CreateAsync(admin, "Admin@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}