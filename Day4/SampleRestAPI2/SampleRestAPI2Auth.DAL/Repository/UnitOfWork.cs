using SampleRestAPI2Auth.DAL.Models;

namespace SampleRestAPI2Auth.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleRestAPI2AuthContext _context;

        public UnitOfWork(SampleRestAPI2AuthContext context)
        {
            _context = context;
        }

        private IGenericRepository<User> _Users;
        public IGenericRepository<User> Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = new GenericRepository<User>(_context);
                }
                return _Users;
            }
        }

        private IGenericRepository<Role> _Roles;
        public IGenericRepository<Role> Roles
        {
            get
            {
                if (_Roles == null)
                {
                    _Roles = new GenericRepository<Role>(_context);
                }
                return _Roles;
            }
        }

        private IGenericRepository<UserRole> _UserRoles;
        public IGenericRepository<UserRole> UserRoles
        {
            get
            {
                if (_UserRoles == null)
                {
                    _UserRoles = new GenericRepository<UserRole>(_context);
                }
                return _UserRoles;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
