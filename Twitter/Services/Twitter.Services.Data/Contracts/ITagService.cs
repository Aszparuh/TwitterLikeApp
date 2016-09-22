namespace Twitter.Services.Data.Contracts
{
    using System.Linq;
    using Twitter.Data.Models;

    public interface ITagService
    {
        IQueryable<Tag> GetAll();
    }
}
