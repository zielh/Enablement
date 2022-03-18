using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MerchantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<Merchants>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<MerchantsWithUserAndCountryDTO> data = await _unitOfWork.Merchants.GetAll()
                .Select(c => new MerchantsWithUserAndCountryDTO
                {
                    CountryId = c.CountryId,
                    Name = c.Name,
                    CountryName = c.Countries.Name,
                    Email = c.Users.Email,
                    FirstName = c.Users.FirstName,
                    LastName = c.Users.LastName,
                    Id = c.Id,
                    UserId = c.UserId
                }).ToListAsync();
            return Ok(data);
        }

        // GET api/<Merchants>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            Merchants data = await _unitOfWork.Merchants.GetBySingle(x => x.Id == id);
            MerchantsDTO result = new MerchantsWithUserAndCountryDTO
            {
                CountryId = data.CountryId,
                Name = data.Name,
                CountryName = data.Countries.Name,
                Email = data.Users.Email,
                FirstName = data.Users.FirstName,
                LastName = data.Users.LastName,
                Id = data.Id,
                UserId = data.UserId
            };
            return Ok(result);
        }

        // POST api/<Merchants>
        [HttpPost]
        public IActionResult Post([FromBody] MerchantsDTO data)
        {
            _unitOfWork.Merchants.Add(new Merchants
            {
                CountryId = data.CountryId,
                CreatedDate = DateTime.Now,
                Id = data.Id,
                Name = data.Name,
                UserId = data.UserId
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<Merchants>/guid
        [HttpPut]
        public IActionResult Put([FromBody] MerchantsDTO data)
        {
            Merchants found = _unitOfWork.Merchants.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return BadRequest();

            found.Name = data.Name;
            _unitOfWork.Merchants.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Merchants>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Merchants found = _unitOfWork.Merchants.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Merchants.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
