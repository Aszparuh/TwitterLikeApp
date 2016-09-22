namespace Twitter.Services.Data
{
    using System.Linq;
    using Contracts;
    using Twitter.Data.Common;
    using Twitter.Data.Models;

    public class TagService : ITagService
    {
        private readonly IDbRepository<Tag> tags;

        public TagService(IDbRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public IQueryable GetAll()
        {
            return this.tags.All();
        }

        public bool Exist(string name)
        {
            return this.tags.Any(x => x.Name == name);
        }
    }
}
