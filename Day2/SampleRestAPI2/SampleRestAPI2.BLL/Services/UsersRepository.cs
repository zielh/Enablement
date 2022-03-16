using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.BLL.Services
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(SampleRestAPI2Context context) : base(context)
        {
        }
        public override string Name => "Users";
        public override async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.Include(c => c.Countries).ToListAsync();
        }
        public override async Task<Users> Get(Guid id)
        {
            return await _context.Users.Include(c => c.Countries).Where(x => id == x.Id).SingleAsync();
        }
    }
}
