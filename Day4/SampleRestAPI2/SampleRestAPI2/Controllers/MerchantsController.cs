using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.BLL.Services;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly MerchantServices _services;
        public MerchantsController(IUnitOfWork unitOfWork)
        {
            _services = new MerchantServices(unitOfWork);
        }

        // GET: api/<Merchants>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _services.Get());
        }

        // GET api/<Merchants>/5
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

        // POST api/<Merchants>
        [HttpPost]
        public IActionResult Post([FromBody] MerchantsDTO data)
        {
            _services.Add(data);
            return Ok();
        }

        // PUT api/<Merchants>/guid
        [HttpPut]
        public IActionResult Put([FromBody] MerchantsDTO data)
        {
            if (!_services.Update(data))
                return BadRequest();
            return Ok();
        }

        // DELETE api/<Merchants>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!_services.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
