namespace M101DotNet.Data.Common.Repositories.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T>
        where T : class
    {
        Task<IQueryable<T>> All();

        Task<T> Get(object id);

        Task<object> Add(T entity);

        Task<object> Update(T entity);

        Task<object> Delete(T entity);

        Task<object> Delete(object id);
    }
}