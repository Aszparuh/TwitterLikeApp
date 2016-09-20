namespace Twitter.Web.ViewModels.Tweet
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTweetViewModel
    {
        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }
    }
}