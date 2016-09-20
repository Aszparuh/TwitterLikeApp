namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;

    public class TweetController : Controller
    {
        // GET: Tweet
        public ActionResult Create()
        {
            return View();
        }
    }
}