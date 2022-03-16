using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.BLL.Repository;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<OrdersItems>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<OrdersItems> data = await _unitOfWork.OrdersItems.GetAll();
            IEnumerable<OrdersItemsDTO> result = data.Select(c => new OrdersItemsWithProductDTO
            {
                Id = c.Id,
                ProductId = c.ProductId,
                ProductName = c.Products.Name,
                Quantity = c.Quantity
            });
            return Ok(result);
        }

        // GET api/<OrdersItems>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            OrdersItems data = await _unitOfWork.OrdersItems.Get(id);
            OrdersItemsDTO result = new OrdersItemsWithProductDTO
            {
                Id = data.Id,
                ProductId = data.ProductId,
                ProductName = data.Products.Name,
                Quantity = data.Quantity
            };
            return Ok(result);
        }

        // POST api/<OrdersItems>
        [HttpPost]
        public IActionResult Post([FromBody] OrdersItemsDTO data)
        {
            _unitOfWork.OrdersItems.Add(new OrdersItems()
            {
                Id = data.Id,
                ProductId = data.ProductId,
                Quantity = data.Quantity
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<OrdersItems>/guid
        [HttpPut]
        public IActionResult Put([FromBody] OrdersItemsDTO data)
        {
            OrdersItems found = _unitOfWork.OrdersItems.Get(data.Id).Result;
            if (found == null)
                return BadRequest();

            found.ProductId = data.ProductId;
            found.Quantity = data.Quantity;
            _unitOfWork.OrdersItems.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<OrdersItems>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            OrdersItems found = _unitOfWork.OrdersItems.Get(id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.OrdersItems.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
