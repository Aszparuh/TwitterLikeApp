namespace Twitter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Tweet : BaseModel<int>, IAuditInfo, IDeletableEntity
    {
        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
