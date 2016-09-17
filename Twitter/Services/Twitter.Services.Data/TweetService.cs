namespace Twitter.Services.Data
{
    using System.Linq;
    using Twitter.Data.Common;
    using Twitter.Data.Models;

    public class TweetService
    {
        private readonly IDbRepository<Tweet> tweets;

        public TweetService(IDbRepository<Tweet> tweets)
        {
            this.tweets = tweets;
        }

        public IQueryable GetAllNew()
        {
            var newestTweets = this.tweets.All().OrderBy(t => t.CreatedOn);
            return newestTweets;
        }
    }
}
