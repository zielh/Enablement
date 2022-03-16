using SampleRestAPI2.DAL;
using SampleRestAPI2.BLL.Interfaces;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.BLL.Services
{
    public class CountriesRepository : GenericRepository<Countries>, ICountriesRepository
    {
        public CountriesRepository(SampleRestAPI2Context context) : base(context)
        {
        }
        public override string Name => "Countries";

    }
}
