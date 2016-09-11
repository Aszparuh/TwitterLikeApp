namespace Twitter.Services.Data
{
    using System;
    using Twitter.Data.Common;
    using Twitter.Data.Models;
    using Twitter.Services.Data.Contracts;

    public class AvatarService : IAvatarService
    {
        public AvatarService(IDbRepository<Image> avatars)
        {

        }

        public Image GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
