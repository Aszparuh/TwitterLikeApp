namespace Twitter.Services.Infrastructure
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Contracts;
    using Data.Models;

    public class TagsExtractionService : ITagExtractionService
    {
        private const string HashTagPattern = @"#([A-Za-z0-9\-_&;]+)";

        public IList<Tag> ExtractTags(string tweetText)
        {
            var tags = new List<Tag>();
            if (tweetText.Contains("#"))
            {
                foreach (Match match in Regex.Matches(tweetText, HashTagPattern))
                {
                    tags.Add(new Tag { Name = match.Value.ToString().ToLower() });
                }
            }

            return tags;
        }
    }
}
