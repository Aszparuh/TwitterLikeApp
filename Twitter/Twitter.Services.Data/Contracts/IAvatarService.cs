namespace Twitter.Services.Data.Contracts
{
    using Twitter.Data.Models;

    public interface IAvatarService
    {
        Image GetById(string id);
    }
}
