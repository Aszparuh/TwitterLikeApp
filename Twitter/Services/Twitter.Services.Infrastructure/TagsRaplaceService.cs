namespace Twitter.Services.Infrastructure
{
    using System.Text.RegularExpressions;

    public class TagsReplaceService
    {
        public string ReplaceHashtagsWithLinks(string originalContent)
        {
            return Regex.Replace(originalContent, "(#)((?:[A-Za-z0-9-_]*))", c => string.Format("<a href=\"/Tag/Index/%23{0} </a>>", c.Value));
        }
    }
}
