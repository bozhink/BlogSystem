namespace Blog.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IQueryable<T>> All();

        Task<T> Get(object id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task Delete(object id);

        Task<int> SaveChanges();
    }
}