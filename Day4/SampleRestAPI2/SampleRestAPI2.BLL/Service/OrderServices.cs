using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class OrderServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrdersWithUsersDTO>> Get()
        {
            return await _unitOfWork.Orders.GetAll()
                     .Select(c => new OrdersWithUsersDTO
                     {
                         Email = c.Users.Email,
                         FirstName = c.Users.FirstName,
                         LastName = c.Users.LastName,
                         Id = c.Id,
                         Status = c.Status,
                         UserId = c.UserId
                     }).ToListAsync();
        }
        public async Task<OrdersWithUsersDTO> GetSingle(Expression<Func<Orders, bool>> where)
        {
            return await _unitOfWork.Orders.GetBySingle(where).ContinueWith((data) =>
            {
                return new OrdersWithUsersDTO
                {
                    Email = data.Result.Users.Email,
                    FirstName = data.Result.Users.FirstName,
                    LastName = data.Result.Users.LastName,
                    Id = data.Result.Id,
                    Status = data.Result.Status,
                    UserId = data.Result.UserId
                };
            });
        }
        public async void Add(OrdersDTO data)
        {
            try
            {
                await _unitOfWork.Orders.Add(new Orders
                {
                    CreatedDate = DateTime.Now,
                    Status = data.Status,
                    UserId = data.UserId
                });
                await _unitOfWork.Complete();
            }
            catch
            {
                throw new Exception($"Error when adding order for {data.UserId}");
            }
        }
        public bool Update(OrdersDTO data)
        {
            Orders found = _unitOfWork.Orders.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return false;
            found.Status = data.Status;
            found.UserId = data.UserId;
            _unitOfWork.Orders.Update(found);
            _unitOfWork.Complete();
            return true;
        }
        public bool Delete(Guid id)
        {
            Orders found = _unitOfWork.Orders.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return false;
            _unitOfWork.Orders.Delete(found);
            _unitOfWork.Complete();
            return true;
        }
    }
}
