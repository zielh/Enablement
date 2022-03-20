using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class CountryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Countries>> Get()
        {
            return await _unitOfWork.Countries.GetAll().ToListAsync();
            //return await _unitOfWork.Countries.GetAll()
            //         .Select(c => new CountriesDTO
            //         {
            //             Id = c.Id,
            //             Name = c.Name
            //         }).ToListAsync();
        }

        public async Task<Countries> GetSingle(Expression<Func<Countries, bool>> where)
        {
            return await _unitOfWork.Countries.GetBySingle(where);
            //return await _unitOfWork.Countries.GetBySingle(where).ContinueWith((data) =>
            //{
            //    return new CountriesDTO
            //    {
            //        Id = data.Result.Id,
            //        Name = data.Result.Name
            //    };
            //});
        }
        public async Task<Countries> GetWithMerchants(Expression<Func<Countries, bool>> where)
        {
            //return await _unitOfWork.Countries.GetAll().Include(x => x.Merchants).Where(where).SingleAsync().ContinueWith((data) =>
            //  {
            //      return new CountriesWithMerchantDTO
            //      {
            //          Id = data.Result.Id,
            //          Name = data.Result.Name,
            //          Merchants = data.Result.Merchants.Select(x => new MerchantsDTO
            //          {
            //              CountryId = x.Id,
            //              Id = x.Id,
            //              Name = x.Name,
            //              UserId = x.UserId

            //          }).ToList()
            //      };
            //  });
            return await _unitOfWork.Countries.GetAll().Include(x => x.Merchants).Where(where).SingleAsync();
        }
        public async Task Add(Countries data)
        {
            try
            {
                await _unitOfWork.Countries.Add(new Countries()
                {
                    Name = data.Name
                });
                await _unitOfWork.Complete();
            }
            catch
            {
                throw new Exception($"Error when adding country {data.Name}");
            }
        }
        public bool Update(Countries data)
        {
            Countries found = _unitOfWork.Countries.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return false;
            found.Name = data.Name;
            _unitOfWork.Countries.Update(found);
            _unitOfWork.Complete();
            return true;
        }
        public bool Delete(Guid id)
        {
            Countries found = _unitOfWork.Countries.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return false;
            _unitOfWork.Countries.Delete(found);
            _unitOfWork.Complete();
            return true;
        }
    }
}
