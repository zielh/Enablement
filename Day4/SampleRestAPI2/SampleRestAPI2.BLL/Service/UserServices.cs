using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class UserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UsersWithCountryDTO>> Get()
        {
            return await _unitOfWork.Users.GetAll()
                     .Select(c => new UsersWithCountryDTO
                     {
                         Id = c.Id,
                         CountryId = c.CountryId,
                         CountryName = c.Countries.Name,
                         DateOfBirth = c.DateOfBirth,
                         Email = c.Email,
                         FirstName = c.FirstName,
                         LastName = c.LastName,
                         Gender = c.Gender
                     }).ToListAsync();
        }
        public async Task<UsersWithCountryDTO> GetSingle(Expression<Func<Users, bool>> where)
        {
            return await _unitOfWork.Users.GetBySingle(where).ContinueWith((data) =>
            {
                return new UsersWithCountryDTO
                {
                    Id = data.Result.Id,
                    CountryId = data.Result.CountryId,
                    CountryName = data.Result.Countries.Name,
                    DateOfBirth = data.Result.DateOfBirth,
                    Email = data.Result.Email,
                    FirstName = data.Result.FirstName,
                    LastName = data.Result.LastName,
                    Gender = data.Result.Gender
                };
            });
        }
        public async void Add(UsersDTO data)
        {
            try
            {
                var result = await _unitOfWork.Users.Add(new Users()
                {
                    CountryId = data.CountryId,
                    CreatedDate = DateTime.Now,
                    DateOfBirth = data.DateOfBirth,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Gender = data.Gender
                });
                await _unitOfWork.Complete();
            }
            catch
            {
                throw new Exception($"Error when adding user {data.FirstName}");
            }
        }
        public bool Update(UsersDTO data)
        {
            Users found = _unitOfWork.Users.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return false;
            found.CountryId = data.CountryId;
            found.DateOfBirth = data.DateOfBirth;
            found.Email = data.Email;
            found.FirstName = data.FirstName;
            found.LastName = data.LastName;
            found.Gender = data.Gender;
            _unitOfWork.Users.Update(found);
            _unitOfWork.Complete();
            return true;
        }
        public bool Delete(Guid id)
        {
            Users found = _unitOfWork.Users.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return false;
            _unitOfWork.Users.Delete(found);
            _unitOfWork.Complete();
            return true;
        }
    }
}