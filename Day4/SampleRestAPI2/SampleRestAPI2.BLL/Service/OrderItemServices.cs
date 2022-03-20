using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class OrderItemServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrdersItemsWithProductAndOrderDTO>> Get()
        {
            return await _unitOfWork.OrdersItems.GetAll()
                     .Select(c => new OrdersItemsWithProductAndOrderDTO
                     {
                         Id = c.Id,
                         OrderId = c.OrderId,
                         Status = c.Orders.Status,
                         ProductId = c.ProductId,
                         ProductName = c.Products.Name,
                         Quantity = c.Quantity
                     }).ToListAsync();
        }
        public async Task<OrdersItemsWithProductAndOrderDTO> GetSingle(Expression<Func<OrdersItems, bool>> where)
        {
            return await _unitOfWork.OrdersItems.GetBySingle(where).ContinueWith((data) =>
            {
                return new OrdersItemsWithProductAndOrderDTO
                {
                    Id = data.Result.Id,
                    OrderId = data.Result.OrderId,
                    Status = data.Result.Orders.Status,
                    ProductId = data.Result.ProductId,
                    ProductName = data.Result.Products.Name,
                    Quantity = data.Result.Quantity
                };
            });
        }
        public async void Add(OrdersItemsDTO data)
        {
            try
            {
                await _unitOfWork.OrdersItems.Add(new OrdersItems()
                {
                    ProductId = data.ProductId,
                    Quantity = data.Quantity
                });
                await _unitOfWork.Complete();
            }
            catch
            {
                throw new Exception($"Error when adding order item for {data.ProductId}");
            }
        }
        public bool Update(OrdersItemsDTO data)
        {
            OrdersItems found = _unitOfWork.OrdersItems.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return false;
            found.ProductId = data.ProductId;
            found.Quantity = data.Quantity;
            _unitOfWork.OrdersItems.Update(found);
            _unitOfWork.Complete();
            return true;
        }
        public bool Delete(Guid id)
        {
            OrdersItems found = _unitOfWork.OrdersItems.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return false;
            _unitOfWork.OrdersItems.Delete(found);
            _unitOfWork.Complete();
            return true;
        }
    }
}
