using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.BLL.Services
{
    public class OrdersRepository : GenericRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(SampleRestAPI2Context context) : base(context)
        {
        }
        public override string Name => "Orders";
        public override async Task<IEnumerable<Orders>> GetAll()
        {
            return await _context.Orders.Include(usr => usr.Users).ToListAsync();
        }
        public override async Task<Orders> Get(Guid id)
        {
            return await _context.Orders.Include(usr => usr.Users).Where(x => id == x.Id).SingleAsync();
        }
    }
}
