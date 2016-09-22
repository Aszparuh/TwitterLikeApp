namespace Twitter.Web.ViewModels.Tweet
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mappings;
    using Twitter.Data.Models;

    public class CreateTweetViewModel : IMapTo<Tweet>
    {
        [Required(ErrorMessage = "Your Tweet is empty")]
        [MaxLength(300, ErrorMessage = "The tweet can't be longer than 300 symbols")]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
