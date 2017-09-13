using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Data.Entity;

namespace Pacha.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
    
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Image> Images { get; set; }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }

    public class AppDbInitiatializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole("admin");
            var role2 = new IdentityRole("user");

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser {  Email = "bartosh.maxim@gmail.com", FirstName = "Maxim", SecondName = "Bartosh"};
            string password = "12345";

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}