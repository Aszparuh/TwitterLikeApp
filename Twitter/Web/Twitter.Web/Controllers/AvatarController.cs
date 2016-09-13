namespace Twitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using Twitter.Data.Models;

    public class AvatarController : Controller
    {
        private const int AvatarStoredWidth = 100;  // ToDo - Change the size of the stored avatar image
        private const int AvatarStoredHeight = 100; // ToDo - Change the size of the stored avatar image
        private const int AvatarScreenWidth = 400;  // ToDo - Change the value of the width of the image on the screen

        private const string TempFolder = "/Temp";
        private const string MapTempFolder = "~" + TempFolder;
        private const string AvatarPath = "/Avatars";

        private readonly string[] imageFileExtensions = { ".jpg", ".png", ".gif", ".jpeg" };
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

        [HttpGet]
        public ActionResult Upload()
        {
            return this.PartialView("_Upload");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null || !files.Any())
            {
                return this.Json(new { success = false, errorMessage = "No file uploaded." });
            }

            var file = files.FirstOrDefault();  // get ONE only
            if (file == null || !this.IsImage(file))
            {
                return this.Json(new { success = false, errorMessage = "File is of wrong format." });
            }

            if (file.ContentLength <= 0)
            {
                return this.Json(new { success = false, errorMessage = "File cannot be zero length." });
            }

            var webPath = this.GetTempSavedFilePath(file);
            return this.Json(new { success = true, fileName = webPath.Replace("/", "\\") }); // success
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string fileName)
        {
            try
            {
                // Calculate dimensions
                var top = Convert.ToInt32(t.Replace("-", string.Empty).Replace("px", string.Empty));
                var left = Convert.ToInt32(l.Replace("-", string.Empty).Replace("px", string.Empty));
                var height = Convert.ToInt32(h.Replace("-", string.Empty).Replace("px", string.Empty));
                var width = Convert.ToInt32(w.Replace("-", string.Empty).Replace("px", string.Empty));

                // Get file from temporary folder
                var fn = Path.Combine(this.Server.MapPath(MapTempFolder), Path.GetFileName(fileName));

                // ...get image and resize it, ...
                var img = new WebImage(fn);
                img.Resize(width, height);

                // ... crop the part the user selected, ...
                img.Crop(top, left, img.Height - top - AvatarStoredHeight, img.Width - left - AvatarStoredWidth);

                // ... delete the temporary file,...
                System.IO.File.Delete(fn);

                // ... and save the new one.
                var userId = this.HttpContext.User.Identity.GetUserId();
                var imageFromDatabase = this.avatars.GetById(userId);
                imageFromDatabase.FileName = img.FileName;
                imageFromDatabase.ContentType = "image/" + img.ImageFormat.ToLower();
                imageFromDatabase.Content = img.GetBytes();
                this.avatars.Save();

                var newFileName = Path.Combine(AvatarPath, Path.GetFileName(fn));
                var newFileLocation = this.HttpContext.Server.MapPath(newFileName);
                if (Directory.Exists(Path.GetDirectoryName(newFileLocation)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation));
                }

                img.Save(newFileLocation);
                return this.Json(new { success = true, avatarFileLocation = newFileName });
            }
            catch (Exception ex)
            {
                return this.Json(new { success = false, errorMessage = "Unable to upload file.\nERRORINFO: " + ex.Message });
            }
        }

        private static string SaveTemporaryAvatarFileImage(HttpPostedFileBase file, string serverPath, string fileName)
        {
            var img = new WebImage(file.InputStream);
            var ratio = img.Height / (double)img.Width;
            img.Resize(AvatarScreenWidth, (int)(AvatarScreenWidth * ratio));

            var fullFileName = Path.Combine(serverPath, fileName);
            if (System.IO.File.Exists(fullFileName))
            {
                System.IO.File.Delete(fullFileName);
            }

            img.Save(fullFileName);
            return Path.GetFileName(img.FileName);
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }

            return file.ContentType.Contains("image") ||
                this.imageFileExtensions.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        private string GetTempSavedFilePath(HttpPostedFileBase file)
        {
            // Define destination
            var serverPath = this.HttpContext.Server.MapPath(TempFolder);
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            // Generate unique file name
            var fileName = Path.GetFileName(file.FileName);
            fileName = SaveTemporaryAvatarFileImage(file, serverPath, fileName);

            // Clean up old files after every save
            this.CleanUpTempFolder(1);
            return Path.Combine(TempFolder, fileName);
        }

        private void CleanUpTempFolder(int hoursOld)
        {
            try
            {
                var currentUtcNow = DateTime.UtcNow;
                var serverPath = this.HttpContext.Server.MapPath("/Temp");
                if (!Directory.Exists(serverPath))
                {
                    return;
                }

                var fileEntries = Directory.GetFiles(serverPath);
                foreach (var fileEntry in fileEntries)
                {
                    var fileCreationTime = System.IO.File.GetCreationTimeUtc(fileEntry);
                    var res = currentUtcNow - fileCreationTime;
                    if (res.TotalHours > hoursOld)
                    {
                        System.IO.File.Delete(fileEntry);
                    }
                }
            }
            catch
            {
                // Deliberately empty.
            }
        }

        private bool CheckStatus304(Image avatar)
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