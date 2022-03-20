using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.Securities;
using SampleRestAPI2.BLL.Services;
using SampleRestAPI2.BLL.DTO;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _services;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _services = new UserServices(unitOfWork);
        }
        // GET: api/<Users>
        [HttpGet]
        [AuthorizedByRole("Admin")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _services.Get());
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        [AuthorizedByRole("Admin")]
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

        // POST api/<Users>
        [HttpPost]
        [AuthorizedByRole("Admin")]
        public IActionResult Post([FromBody] UsersDTO data)
        {
            _services.Add(data);
            return Ok();
        }

        // PUT api/<Users>/guid
        [HttpPut]
        [AuthorizedByRole("Admin")]
        public IActionResult Put([FromBody] UsersDTO data)
        {
            if (!_services.Update(data))
                return BadRequest();
            return Ok();
        }

        // DELETE api/<Users>/guid
        [HttpDelete]
        [AuthorizedByRole("Admin")]
        public IActionResult Delete(Guid id)
        {
            if (!_services.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
