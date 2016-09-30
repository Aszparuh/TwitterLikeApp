namespace Twitter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<Twitter.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Twitter.Data.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }

                // Create admin user
                var user = userManager.FindByName("admin");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "admin@admin.bg",
                        Email = "admin@admin.bg",
                        PhoneNumber = "5551234567",
                    };

                    string filePath = this.MapPath("~/App_Data/Resources/avatar.png");
                    var image = System.Drawing.Image.FromFile(filePath);

                    byte[] imageContent;
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, image.RawFormat);
                        imageContent = ms.ToArray();
                    }

                    newUser.Avatar = new Image { FileName = "avatar", ContentType = "image/png", Content = imageContent, CreatedOn = DateTime.Now };

                    userManager.Create(newUser, "Password1");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }

            if (!context.Tweets.Any())
            {
                var admin = context.Users.FirstOrDefault(u => u.UserName == "admin@admin.bg");

                var tweets = new Tweet[]
                {
                    new Tweet { OriginalContent = "Initial Tweet by Admin " + DateTime.Now.ToString() },
                    new Tweet { OriginalContent = "Second Tweet" },
                    new Tweet { OriginalContent = "Third Tweet" }
                };

                foreach (var tweet in tweets)
                {
                    admin.Tweets.Add(tweet);
                }

                context.SaveChanges();
            }
        }

        private string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
            {
                return HostingEnvironment.MapPath(seedFile);
            }

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, @"..\..\..\..\Web\Twitter.Web\" + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }
    }
}
