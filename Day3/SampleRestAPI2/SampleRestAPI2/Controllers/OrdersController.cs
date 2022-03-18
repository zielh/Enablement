using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.DAL.Repository;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<Orders>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<OrdersWithUsersDTO> data = await _unitOfWork.Orders.GetAll()
                .Select(c => new OrdersWithUsersDTO
                {

                    Email = c.Users.Email,
                    FirstName = c.Users.FirstName,
                    LastName = c.Users.LastName,
                    Id = c.Id,
                    Status = c.Status,
                    UserId = c.UserId
                }).ToListAsync();
            return Ok(data);
        }

        // GET api/<Orders>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            Orders data = await _unitOfWork.Orders.GetBySingle(x => x.Id == id);
            OrdersDTO result = new OrdersWithUsersDTO
            {
                Email = data.Users.Email,
                FirstName = data.Users.FirstName,
                LastName = data.Users.LastName,
                Id = data.Id,
                Status = data.Status,
                UserId = data.UserId
            };
            return Ok(result);
        }

        // POST api/<Orders>
        [HttpPost]
        public IActionResult Post([FromBody] OrdersDTO data)
        {
            _unitOfWork.Orders.Add(new Orders
            {
                CreatedDate = DateTime.Now,
                Id = data.Id,
                Status = data.Status,
                UserId = data.UserId
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<Orders>/guid
        [HttpPut]
        public IActionResult Put([FromBody] OrdersDTO data)
        {
            Orders found = _unitOfWork.Orders.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return BadRequest();

            found.Status = data.Status;
            found.UserId = data.UserId;
            _unitOfWork.Orders.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Orders>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Orders found = _unitOfWork.Orders.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Orders.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
