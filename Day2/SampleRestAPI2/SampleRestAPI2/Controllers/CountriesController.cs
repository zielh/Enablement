using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.BLL.Repository;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<Countries>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<Countries> data = await _unitOfWork.Countries.GetAll();
            IEnumerable<CountriesDTO> result = data.Select(c => new CountriesDTO
            {
                Id = c.Id,
                Name = c.Name
            });
            return Ok(result);
        }

        // GET api/<Countries>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            Countries data = await _unitOfWork.Countries.Get(id);
            CountriesDTO result = new CountriesDTO
            {
                Id = data.Id,
                Name = data.Name
            };
            return Ok(result);
        }

        // POST api/<Countries>
        [HttpPost]
        public IActionResult Post([FromBody] CountriesDTO data)
        {
            _unitOfWork.Countries.Add(new Countries()
            {
                Id = data.Id,
                Name = data.Name
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<Countries>/guid
        [HttpPut]
        public IActionResult Put([FromBody] CountriesDTO data)
        {
            Countries found = _unitOfWork.Countries.Get(data.Id).Result;
            if (found == null)
                return BadRequest();

            found.Name = data.Name;
            _unitOfWork.Countries.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Countries>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Countries found = _unitOfWork.Countries.Get(id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Countries.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
