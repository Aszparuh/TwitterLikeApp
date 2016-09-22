namespace Twitter.Data.Common
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Models;

    public interface IDbRepository<T> : IDbRepository<T, int>
        where T : BaseModel<int>
    {
    }

    public interface IDbRepository<T, in TKey>
        where T : BaseModel<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
