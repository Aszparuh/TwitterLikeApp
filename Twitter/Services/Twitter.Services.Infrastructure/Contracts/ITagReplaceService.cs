namespace Twitter.Services.Infrastructure.Contracts
{
    public interface ITagReplaceService
    {
        string ReplaceHashtagsWithLinks(string originalContent);
    }
}
