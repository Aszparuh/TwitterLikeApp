namespace Twitter.Services.Infrastructure
{
    using System.Text.RegularExpressions;
    using Contracts;

    public class TagsReplaceService : ITagReplaceService
    {
        public string ReplaceHashtagsWithLinks(string originalContent)
        {
            return Regex.Replace(
                originalContent,
                "(#)((?:[A-Za-z0-9-_]*))",
                c => string.Format(
                    "<a href=\"/Tag/Index/%23{0}\">{1}</a>",
                c.Value.TrimStart(new char[] { '#' }),
                c.Value));
        }
    }
}
