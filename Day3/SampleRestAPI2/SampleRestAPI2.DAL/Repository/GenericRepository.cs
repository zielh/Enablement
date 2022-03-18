using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SampleRestAPI2.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SampleRestAPI2Context _context;
        public GenericRepository(SampleRestAPI2Context context)
        {
            _context = context;
        }
        public virtual async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().Where(where).ToListAsync();
        }
        public virtual async Task<T> GetBySingle(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().Where(where).SingleAsync();
        }
        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
