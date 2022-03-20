using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleRestAPI2Context _context;

        public UnitOfWork(SampleRestAPI2Context context)
        {
            _context = context;
        }

        private IGenericRepository<Countries> _Countries;
        public IGenericRepository<Countries> Countries
        {
            get
            {
                if (_Countries == null)
                {
                    _Countries = new GenericRepository<Countries>(_context);
                }
                return _Countries;
            }
        }

        private IGenericRepository<Merchants> _Merchants;
        public IGenericRepository<Merchants> Merchants
        {
            get
            {
                if (_Merchants == null)
                {
                    _Merchants = new GenericRepository<Merchants>(_context);
                }
                return _Merchants;
            }
        }

        private IGenericRepository<Orders> _Orders;
        public IGenericRepository<Orders> Orders
        {
            get
            {
                if (_Orders == null)
                {
                    _Orders = new GenericRepository<Orders>(_context);
                }
                return _Orders;
            }
        }

        private IGenericRepository<OrdersItems> _OrdersItems;
        public IGenericRepository<OrdersItems> OrdersItems
        {
            get
            {
                if (_OrdersItems == null)
                {
                    _OrdersItems = new GenericRepository<OrdersItems>(_context);
                }
                return _OrdersItems;
            }
        }

        private IGenericRepository<Products> _Products;
        public IGenericRepository<Products> Products
        {
            get
            {
                if (_Products == null)
                {
                    _Products = new GenericRepository<Products>(_context);
                }
                return _Products;
            }
        }

        private IGenericRepository<Users> _Users;
        public IGenericRepository<Users> Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = new GenericRepository<Users>(_context);
                }
                return _Users;
            }
        }

        private IGenericRepository<Logs> _Logs;
        public IGenericRepository<Logs> Logs
        {
            get
            {
                if (_Logs == null)
                {
                    _Logs = new GenericRepository<Logs>(_context);
                }
                return _Logs;
            }
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
