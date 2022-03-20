
using SampleRestAPI2Auth.DAL.Models;

namespace SampleRestAPI2Auth.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<UserRole> UserRoles { get; }
        int Complete();
    }
}
