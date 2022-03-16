using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL;

namespace SampleRestAPI2.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleRestAPI2Context _context;

        public UnitOfWork(SampleRestAPI2Context context)
        {
            _context = context;
        }

        private ICountriesRepository _Countries;
        public ICountriesRepository Countries
        {
            get
            {
                if (_Countries == null)
                {
                    _Countries = new Services.CountriesRepository(_context);
                }
                return _Countries;
            }
        }

        private IMerchantsRepository _Merchants;
        public IMerchantsRepository Merchants
        {
            get
            {
                if (_Merchants == null)
                {
                    _Merchants = new Services.MerchantsRepository(_context);
                }
                return _Merchants;
            }
        }

        private IOrdersRepository _Orders;
        public IOrdersRepository Orders
        {
            get
            {
                if (_Orders == null)
                {
                    _Orders = new Services.OrdersRepository(_context);
                }
                return _Orders;
            }
        }

        private IOrdersItemsRepository _OrdersItems;
        public IOrdersItemsRepository OrdersItems
        {
            get
            {
                if (_OrdersItems == null)
                {
                    _OrdersItems = new Services.OrdersItemsRepository(_context);
                }
                return _OrdersItems;
            }
        }

        private IProductsRepository _Products;
        public IProductsRepository Products
        {
            get
            {
                if (_Products == null)
                {
                    _Products = new Services.ProductsRepository(_context);
                }
                return _Products;
            }
        }

        private IUsersRepository _Users;
        public IUsersRepository Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = new Services.UsersRepository(_context);
                }
                return _Users;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
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
