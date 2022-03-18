
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Countries> Countries { get; }
        IGenericRepository<Merchants> Merchants { get; }
        IGenericRepository<Orders> Orders { get; }
        IGenericRepository<OrdersItems> OrdersItems { get; }
        IGenericRepository<Products> Products { get; }
        IGenericRepository<Users> Users { get; }
        int Complete();
    }
}
