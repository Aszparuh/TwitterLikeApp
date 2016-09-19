namespace Twitter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Data.Common.Models;

    public class Tag : BaseModel<int>, IAuditInfo, IDeletableEntity
    {
        private ICollection<Tweet> tweets;

        public Tag()
        {
            this.tweets = new HashSet<Tweet>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string Name { get; set; }

        public virtual ICollection<Tweet> Tweets
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
    }
}
