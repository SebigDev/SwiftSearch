using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SwiftSearch.Data;

namespace SwiftSearch.Models
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

    public class SwiftSearchDbContext : IdentityDbContext<ApplicationUser>
    {
        public SwiftSearchDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SwiftSearchDbContext Create()
        {
            return new SwiftSearchDbContext();
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
    }
}