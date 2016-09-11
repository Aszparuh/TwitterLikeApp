namespace Twitter.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Twitter.Data.Models;

    public class AvatarController : Controller
    {
        private readonly IAvatarService avatars;

        public AvatarController(IAvatarService avatars)
        {
            this.avatars = avatars;
        }

        public ActionResult Index(string id)
        {
            var avatar = this.avatars.GetById(id);
            if (this.CheckStatus304(avatar))
            {
                return this.Content(string.Empty);
            }

            return this.File(avatar.Content, avatar.ContentType);
        }

        protected bool CheckStatus304(Image avatar)
        {
            // http://weblogs.asp.net/jeff/304-your-images-from-a-database
            DateTime lastModified;
            if (avatar.ModifiedOn == null)
            {
                lastModified = avatar.CreatedOn;
            }
            else
            {
                lastModified = (DateTime)avatar.ModifiedOn;
            }

            if (!string.IsNullOrEmpty(this.Request.Headers["If-Modified-Since"]))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var lastMod = DateTime.ParseExact(this.Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();
                if (lastMod == lastModified.AddMilliseconds(-lastModified.Millisecond))
                {
                    this.Response.StatusCode = 304;
                    this.Response.StatusDescription = "Not Modified";
                    return true;
                }
            }

            this.Response.Cache.SetCacheability(HttpCacheability.Public);
            this.Response.Cache.SetLastModified(lastModified);

            return false;
        }
    }
}