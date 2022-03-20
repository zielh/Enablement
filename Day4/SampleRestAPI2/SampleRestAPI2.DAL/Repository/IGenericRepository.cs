using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace SampleRestAPI2.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetBySingle(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        Task<T> Add(T entity);
        T Delete(T entity);
        T Update(T entity);
    }
}
