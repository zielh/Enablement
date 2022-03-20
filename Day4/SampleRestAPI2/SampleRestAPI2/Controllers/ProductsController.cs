using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.BLL.Services;
using SampleRestAPI2.BLL.DTO;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductServices _services;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _services = new ProductServices(unitOfWork);
        }

        // GET: api/<Products>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _services.Get());
        }

        // GET api/<Products>/5
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

        // POST api/<Products>
        [HttpPost]
        public IActionResult Post([FromBody] ProductsDTO data)
        {
            _services.Add(data);
            return Ok();
        }

        // PUT api/<Products>/guid
        [HttpPut]
        public IActionResult Put([FromBody] ProductsDTO data)
        {
            if (!_services.Update(data))
                return BadRequest();
            return Ok();
        }

        // DELETE api/<Products>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!_services.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
