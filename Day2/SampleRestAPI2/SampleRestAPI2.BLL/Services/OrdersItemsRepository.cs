using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.BLL.Services
{
    public class OrdersItemsRepository : GenericRepository<OrdersItems>, IOrdersItemsRepository
    {
        public OrdersItemsRepository(SampleRestAPI2Context context) : base(context)
        {
        }
        public override string Name => "OrdersItems";
        public override async Task<IEnumerable<OrdersItems>> GetAll()
        {
            return await _context.OrdersItems.Include(prod => prod.Products).ToListAsync();
        }
        public override async Task<OrdersItems> Get(Guid id)
        {
            return await _context.OrdersItems.Include(prod => prod.Products).Where(x => id == x.Id).SingleAsync();
        }
    }
}