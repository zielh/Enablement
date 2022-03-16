using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.BLL.Repository;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<Products>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<Products> data = await _unitOfWork.Products.GetAll();
            IEnumerable<ProductsDTO> result = data.Select(c => new ProductsWithMerchantsDTO
            {
                Id = c.Id,
                Name = c.Name,
                MerchantId = c.MerchantId,
                MerchantName = c.Merchants.Name,
                Price = c.Price,
                Status = c.Status
            });
            return Ok(result);
        }

        // GET api/<Products>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            Products data = await _unitOfWork.Products.Get(id);
            ProductsDTO result = new ProductsWithMerchantsDTO
            {
                Id = data.Id,
                Name = data.Name,
                MerchantId = data.MerchantId,
                MerchantName = data.Merchants.Name,
                Price = data.Price,
                Status = data.Status
            };
            return Ok(result);
        }

        // POST api/<Products>
        [HttpPost]
        public IActionResult Post([FromBody] ProductsDTO data)
        {
            _unitOfWork.Products.Add(new Products()
            {
                Id = data.Id,
                CreatedDate = DateTime.Now,
                MerchantId = data.MerchantId,
                Name = data.Name,
                Price = data.Price,
                Status = data.Status
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<Products>/guid
        [HttpPut]
        public IActionResult Put([FromBody] ProductsDTO data)
        {
            Products found = _unitOfWork.Products.Get(data.Id).Result;
            if (found == null)
                return BadRequest();

            found.Name = data.Name;
            found.MerchantId = data.MerchantId;
            found.Price = data.Price;
            found.Status = data.Status;
            _unitOfWork.Products.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Products>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Products found = _unitOfWork.Products.Get(id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Products.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
