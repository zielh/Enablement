using System.Linq.Expressions;

namespace SampleRestAPI2.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetBySingle(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
