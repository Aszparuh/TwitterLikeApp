namespace Twitter.Services.Data.Contracts
{
    using System.Linq;

    public interface ITweetService
    {
        IQueryable GetAllNew();
    }
}
