namespace Twitter.Services.Data
{
    using Twitter.Data.Common;
    using Twitter.Data.Models;
    using Twitter.Services.Data.Contracts;

    public class AvatarService : IAvatarService
    {
        private readonly IDbRepository<Image, string> avatars;

        public AvatarService(IDbRepository<Image, string> avatars)
        {
            this.avatars = avatars;
        }

        public Image GetById(string id)
        {
            return this.avatars.GetById(id);
        }

        public void Save()
        {
            this.avatars.Save();
        }
    }
}
