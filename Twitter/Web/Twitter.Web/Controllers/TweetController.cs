namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Services.Infrastructure.Contracts;
    using ViewModels.Tweet;

    public class TweetController : Controller
    {
        private readonly ITagExtractionService tagExtractor;
        private readonly ITagService tags;

        public TweetController(ITagExtractionService tagExtractor, ITagService tags)
        {
            this.tagExtractor = tagExtractor;
            this.tags = tags;
        }

        // GET: Tweet
        public ActionResult Create(CreateTweetViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var tags = this.tagExtractor.ExtractTags(model.Content);
                if (tags.Count > 0)
                {
                    foreach (var tag in tags)
                    {
                        if (this.tags.Exist(tag.Name))
                        {

                        }
                    }
                }
            }

            return this.View("_CreateTweetPartial", model);
        }
    }
}