using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;

namespace SampleRestAPI2.BLL.Services
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SampleRestAPI2Context _context;
        public abstract string Name { get; }
        public GenericRepository(SampleRestAPI2Context context)
        {
            _context = context;
        }
        public virtual async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
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
