using SampleRestAPI2.BLL.Interfaces;

namespace SampleRestAPI2.BLL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICountriesRepository Countries { get; }
        IMerchantsRepository Merchants { get; }
        IOrdersRepository Orders { get; }
        IOrdersItemsRepository OrdersItems { get; }
        IProductsRepository Products { get; }
        IUsersRepository Users { get; }
        int Complete();
    }
}
