using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class MerchantServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public MerchantServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<MerchantsWithUserAndCountryDTO>> Get()
        {
            return await _unitOfWork.Merchants.GetAll()
                     .Select(c => new MerchantsWithUserAndCountryDTO
                     {
                         CountryId = c.CountryId,
                         Name = c.Name,
                         CountryName = c.Countries.Name,
                         Email = c.Users.Email,
                         FirstName = c.Users.FirstName,
                         LastName = c.Users.LastName,
                         Id = c.Id,
                         UserId = c.UserId
                     }).ToListAsync();
        }

        public async Task<MerchantsWithUserAndCountryDTO> GetSingle(Expression<Func<Merchants, bool>> where)
        {
            return await _unitOfWork.Merchants.GetBySingle(where).ContinueWith((data) =>
            {
                return new MerchantsWithUserAndCountryDTO
                {
                    CountryId = data.Result.CountryId,
                    Name = data.Result.Name,
                    CountryName = data.Result.Countries.Name,
                    Email = data.Result.Users.Email,
                    FirstName = data.Result.Users.FirstName,
                    LastName = data.Result.Users.LastName,
                    Id = data.Result.Id,
                    UserId = data.Result.UserId
                };
            });
        }
        public async void Add(MerchantsDTO data)
        {
            try
            {
                await _unitOfWork.Merchants.Add(new Merchants
                {
                    CountryId = data.CountryId,
                    CreatedDate = DateTime.Now,
                    Name = data.Name,
                    UserId = data.UserId
                });
                await _unitOfWork.Complete();
            }
            catch
            {
                throw new Exception($"Error when adding merchant {data.Name}");
            }
        }
        public bool Update(MerchantsDTO data)
        {
            Merchants found = _unitOfWork.Merchants.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return false;
            found.Name = data.Name;
            _unitOfWork.Merchants.Update(found);
            _unitOfWork.Complete();
            return true;
        }
        public bool Delete(Guid id)
        {
            Merchants found = _unitOfWork.Merchants.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return false;
            _unitOfWork.Merchants.Delete(found);
            _unitOfWork.Complete();
            return true;
        }
    }
}
