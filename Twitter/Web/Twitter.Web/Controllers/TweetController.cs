namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Infrastructure.Contracts;
    using ViewModels.Tweet;

    public class TweetController : Controller
    {
        private readonly ITagExtractionService tagExtractor;

        public TweetController(ITagExtractionService tagExtractor)
        {
            this.tagExtractor = tagExtractor;
        }

        // GET: Tweet
        public ActionResult Create(CreateTweetViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var tags = this.tagExtractor.ExtractTags(model.Content);
                if (tags.Count > 0)
                {

                }
            }

            return this.View("_CreateTweetPartial", model);
        }
    }
}