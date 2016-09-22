namespace Twitter.Services.Infrastructure.Contracts
{
    using System.Collections.Generic;
    using Data.Models;

    public interface ITagExtractionService
    {
        IList<Tag> ExtractTags(string tweetText);
    }
}
