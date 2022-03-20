using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class ProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductsWithMerchantsDTO>> Get()
        {
            return await _unitOfWork.Products.GetAll()
                     .Select(c => new ProductsWithMerchantsDTO
                     {
                         Id = c.Id,
                         Name = c.Name,
                         MerchantId = c.MerchantId,
                         MerchantName = c.Merchants.Name,
                         Price = c.Price,
                         Status = c.Status
                     }).ToListAsync();
        }
        public async Task<ProductsWithMerchantsDTO> GetSingle(Expression<Func<Products, bool>> where)
        {
            return await _unitOfWork.Products.GetBySingle(where).ContinueWith((data) =>
            {
                return new ProductsWithMerchantsDTO
                {
                    Id = data.Result.Id,
                    Name = data.Result.Name,
                    MerchantId = data.Result.MerchantId,
                    MerchantName = data.Result.Merchants.Name,
                    Price = data.Result.Price,
                    Status = data.Result.Status
                };
            });
        }
        public async void Add(ProductsDTO data)
        {
            try
            {
                await _unitOfWork.Products.Add(new Products()
                {
                    CreatedDate = DateTime.Now,
                    MerchantId = data.MerchantId,
                    Name = data.Name,
                    Price = data.Price,
                    Status = data.Status
                });
                await _unitOfWork.Complete();
            }
            catch
            {
                throw new Exception($"Error when adding product {data.Name}");
            }
        }
        public bool Update(ProductsDTO data)
        {
            Products found = _unitOfWork.Products.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return false;
            found.Name = data.Name;
            found.MerchantId = data.MerchantId;
            found.Price = data.Price;
            found.Status = data.Status;
            _unitOfWork.Products.Update(found);
            _unitOfWork.Complete();
            return true;
        }
        public bool Delete(Guid id)
        {
            Products found = _unitOfWork.Products.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return false;
            _unitOfWork.Products.Delete(found);
            _unitOfWork.Complete();
            return true;
        }
    }
}
