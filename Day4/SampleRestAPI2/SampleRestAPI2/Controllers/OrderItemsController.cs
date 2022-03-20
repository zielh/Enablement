using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.BLL.Services;
using SampleRestAPI2.BLL.DTO;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemServices _services;
        public OrderItemsController(IUnitOfWork unitOfWork)
        {
            _services = new OrderItemServices(unitOfWork);
        }

        // GET: api/<OrdersItems>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _services.Get());
        }

        // GET api/<OrdersItems>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _services.GetSingle(x => id == x.Id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<OrdersItems>
        [HttpPost]
        public IActionResult Post([FromBody] OrdersItemsDTO data)
        {
            _services.Add(data);
            return Ok();
        }

        // PUT api/<OrdersItems>/guid
        [HttpPut]
        public IActionResult Put([FromBody] OrdersItemsDTO data)
        {

            if (!_services.Update(data))
                return BadRequest();
            return Ok();
        }

        // DELETE api/<OrdersItems>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!_services.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
