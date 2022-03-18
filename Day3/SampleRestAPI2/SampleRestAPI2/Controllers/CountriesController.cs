using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using SampleRestAPI2.DAL.Repository;
using Microsoft.EntityFrameworkCore;

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
        [Authorize]
        public async Task<ActionResult> Get()
        {
            IEnumerable<CountriesDTO> data = await _unitOfWork.Countries.GetAll()
                .Select(c => new CountriesDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
            return Ok(data);
        }

        // GET api/<Countries>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            Countries data = await _unitOfWork.Countries.GetBySingle(x => x.Id == id);
            CountriesDTO result = new CountriesDTO
            {
                Id = data.Id,
                Name = data.Name
            };
            return Ok(result);
        }

        // GET api/<Countries>/WithMerchants
        [HttpGet]
        [Route("WithMerchants")]
        [Authorize]
        public async Task<ActionResult> GetWithMerchants(Guid id)
        {
            Countries data = await _unitOfWork.Countries.GetAll().Include(x => x.Merchants).Where(y => y.Id == id).SingleAsync();
            CountriesWithMerchantDTO result = new CountriesWithMerchantDTO
            {
                Id = data.Id,
                Name = data.Name,
                Merchants = data.Merchants.Select(x => new MerchantsDTO
                {
                    CountryId = x.Id,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId

                }).ToList()
            };
            return Ok(result);
        }

        // POST api/<Countries>
        [HttpPost]
        [Authorize]
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
        [Authorize]
        public IActionResult Put([FromBody] CountriesDTO data)
        {
            Countries found = _unitOfWork.Countries.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return BadRequest();

            found.Name = data.Name;
            _unitOfWork.Countries.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Countries>/guid
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            Countries found = _unitOfWork.Countries.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Countries.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
