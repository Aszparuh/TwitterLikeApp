namespace Twitter.Web.ViewModels.Home
{
    using Data.Models;
    using Infrastructure.Mappings;

    public class TweetViewModel : IMapFrom<Tweet>
    {
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }
    }
}