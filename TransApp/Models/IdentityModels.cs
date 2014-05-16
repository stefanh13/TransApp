using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TransApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("VideoContext")
        {
        }

        public DbSet<Video> videos { get; set; }
        public DbSet<Translation> translations { get; set; }
        public DbSet<UserRequest> userRequest { get; set; }
        public DbSet<Comment> comments { get; set; }
    }
}