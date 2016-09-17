namespace Twitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Mappings;
    using Services.Data.Contracts;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ITweetService tweets;

        public HomeController(ITweetService tweets)
        {
            this.tweets = tweets;
        }

        public ActionResult Index()
        {
            var homePageTweets = this.tweets.GetAllNew().To<TweetViewModel>();
            return this.View(homePageTweets);
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}