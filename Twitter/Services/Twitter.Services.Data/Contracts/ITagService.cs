namespace Twitter.Services.Data.Contracts
{
    using System.Linq;

    public interface ITagService
    {
        IQueryable GetAll();

        bool Exist(string name);
    }
}
