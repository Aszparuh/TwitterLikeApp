namespace Twitter.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using Twitter.Data.Common;
    using Twitter.Data.Models;

    public class TweetService : ITweetService
    {
        private readonly IDbRepository<Tweet> tweets;

        public TweetService(IDbRepository<Tweet> tweets)
        {
            this.tweets = tweets;
        }

        public IQueryable<Tweet> GetAllNew()
        {
            var newestTweets = this.tweets.All().OrderBy(t => t.CreatedOn);
            return newestTweets;
        }

        void ITweetService.Add(Tweet tweet)
        {
            this.tweets.Add(tweet);
        }
    }
}
