namespace Twitter.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Home;

    public class TagController : BaseController
    {
        private ITagService tags;

        public TagController(ITagService tags)
        {
            this.tags = tags;
        }

        public ActionResult Index(string id)
        {
            var tag = this.tags
                .GetAll()
                .Include(t => t.Tweets)
                .FirstOrDefault(t => t.Name == id);

            if (tag == null)
            {
                return this.HttpNotFound("Tag not found");
            }
            else
            {
                var tweets = this.Mapper.Map<IEnumerable<TweetViewModel>>(tag.Tweets);
                return this.View(tweets);
            }
        }
    }
}