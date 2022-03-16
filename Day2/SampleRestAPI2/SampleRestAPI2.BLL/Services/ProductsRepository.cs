using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.BLL.Services
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(SampleRestAPI2Context context) : base(context)
        {
        }
        public override string Name => "Products";
        public override async Task<IEnumerable<Products>> GetAll()
        {
            return await _context.Products.Include(mer => mer.Merchants).ToListAsync();
        }
        public override async Task<Products> Get(Guid id)
        {
            return await _context.Products.Include(mer => mer.Merchants).Where(x => id == x.Id).SingleAsync();
        }
    }
}
