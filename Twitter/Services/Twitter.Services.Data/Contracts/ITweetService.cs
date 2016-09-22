namespace Twitter.Services.Data.Contracts
{
    using System.Linq;
    using Twitter.Data.Models;

    public interface ITweetService
    {
        IQueryable<Tweet> GetAllNew();

        void Add(Tweet tweet);
    }
}
