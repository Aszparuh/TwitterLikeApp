namespace Twitter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;

    public class Image : BaseModel<string>, IAuditInfo, IDeletableEntity
    {
        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        [Key]
        [ForeignKey("User")]
        public override string Id { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
