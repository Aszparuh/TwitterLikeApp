namespace Twitter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using Services.Infrastructure.Contracts;
    using ViewModels.Tweet;

    public class TweetController : BaseController
    {
        private readonly ITagExtractionService tagExtractor;
        private readonly ITagService tags;
        private readonly ITweetService tweets;
        private readonly ITagReplaceService tagRepalceService;

        public TweetController(
            ITagExtractionService tagExtractor,
            ITagService tags,
            ITweetService tweets,
            ITagReplaceService tagRepalceService)
        {
            this.tagExtractor = tagExtractor;
            this.tags = tags;
            this.tweets = tweets;
            this.tagRepalceService = tagRepalceService;
        }

        // GET: Tweet
        public ActionResult Create(CreateTweetViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.AddNewTweet(model);
                return this.RedirectToAction("Index", "Home");
            }

            return this.View("_CreateTweetPartial", model);
        }

        private void AddNewTweet(CreateTweetViewModel model)
        {
            var tweetToSave = this.Mapper.Map<Tweet>(model);
            tweetToSave.ApplicationUserId = this.User.Identity.GetUserId();
            var tags = this.tagExtractor.ExtractTags(tweetToSave.OriginalContent);
            if (tags.Count > 0)
            {
                foreach (var tag in tags)
                {
                    var tagFromDatabase = this.tags.GetAll().FirstOrDefault(t => t.Name == tag.Name);
                    if (tagFromDatabase != null)
                    {
                        tweetToSave.Tags.Add(tagFromDatabase);
                    }
                    else
                    {
                        tweetToSave.Tags.Add(tag);
                    }
                }
            }

            var displayContent = this.tagRepalceService.ReplaceHashtagsWithLinks(tweetToSave.OriginalContent);
            tweetToSave.DisplayContent = displayContent;

            this.tweets.Add(tweetToSave);
        }
    }
}