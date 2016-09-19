namespace Twitter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Tweet : BaseModel<int>, IAuditInfo, IDeletableEntity
    {
        private ICollection<Tag> tags;

        public Tweet()
        {
            this.tags = new HashSet<Tag>();
        }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get
            {
                return this.tags;
            }

            set
            {
                this.tags = value;
            }
        }
    }
}
