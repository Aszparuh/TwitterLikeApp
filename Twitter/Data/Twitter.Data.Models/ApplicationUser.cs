namespace Twitter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IDeletableEntity, IAuditInfo
    {
        private ICollection<Tweet> tweets;

        public ApplicationUser()
        {
            // TODO: maybe UtcNow, but left it like this for consistency with other code
            this.CreatedOn = DateTime.Now;
            this.tweets = new HashSet<Tweet>();
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ICollection<Tweet> Tweets
        {
            get
            {
                return this.tweets;
            }

            set
            {
                this.tweets = value;
            }
        }

        public virtual Image Avatar { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}