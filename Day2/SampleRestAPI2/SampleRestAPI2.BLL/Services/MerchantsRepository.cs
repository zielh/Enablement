using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.BLL.Services
{
    public class MerchantsRepository : GenericRepository<Merchants>, IMerchantsRepository
    {
        public MerchantsRepository(SampleRestAPI2Context context) : base(context)
        {
        }
        public override string Name => "Merchants";
        public override async Task<IEnumerable<Merchants>> GetAll()
        {
            return await _context.Merchants.Include(usr => usr.Users).Include(c => c.Countries).ToListAsync();
        }
        public override async Task<Merchants> Get(Guid id)
        {
            return await _context.Merchants.Include(usr => usr.Users).Include(c => c.Countries).Where(x => id == x.Id).SingleAsync();
        }
    }
}
