using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.BLL.Services;
using SampleRestAPI2.BLL.DTO;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderServices _services;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _services = new OrderServices(unitOfWork);
        }

        // GET: api/<Orders>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _services.Get());
        }

        // GET api/<Orders>/5
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

        // POST api/<Orders>
        [HttpPost]
        public IActionResult Post([FromBody] OrdersDTO data)
        {
            _services.Add(data);
            return Ok();
        }

        // PUT api/<Orders>/guid
        [HttpPut]
        public IActionResult Put([FromBody] OrdersDTO data)
        {
            if (!_services.Update(data))
                return BadRequest();
            return Ok();
        }

        // DELETE api/<Orders>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!_services.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
