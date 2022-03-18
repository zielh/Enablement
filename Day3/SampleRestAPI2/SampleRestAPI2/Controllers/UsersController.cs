using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.Securities;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<Users>
        [HttpGet]
        [AuthorizedByRole("Admin")]
        public async Task<ActionResult> Get()
        {
            IEnumerable<UsersWithCountryDTO> data = await _unitOfWork.Users.GetAll()
                .Select(c => new UsersWithCountryDTO
                {
                    Id = c.Id,
                    CountryId = c.CountryId,
                    CountryName = c.Countries.Name,
                    DateOfBirth = c.DateOfBirth,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Gender = c.Gender
                }).ToListAsync();
            return Ok(data);
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        [AuthorizedByRole("Admin")]
        public async Task<ActionResult> Get(Guid id)
        {
            Users data = await _unitOfWork.Users.GetBySingle(x => x.Id == id);
            UsersDTO result = new UsersWithCountryDTO
            {
                Id = data.Id,
                CountryId = data.CountryId,
                CountryName = data.Countries.Name,
                DateOfBirth = data.DateOfBirth,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Gender = data.Gender
            };
            return Ok(result);
        }

        // POST api/<Users>
        [HttpPost]
        [AuthorizedByRole("Admin")]
        public IActionResult Post([FromBody] UsersDTO data)
        {
            _unitOfWork.Users.Add(new Users()
            {
                CountryId = data.CountryId,
                CreatedDate = DateTime.Now,
                DateOfBirth = data.DateOfBirth,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Gender = data.Gender,
                Id = data.Id
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<Users>/guid
        [HttpPut]
        [AuthorizedByRole("Admin")]
        public IActionResult Put([FromBody] UsersDTO data)
        {
            Users found = _unitOfWork.Users.GetBySingle(x => x.Id == data.Id).Result;
            if (found == null)
                return BadRequest();

            found.CountryId = data.CountryId;
            found.DateOfBirth = data.DateOfBirth;
            found.Email = data.Email;
            found.FirstName = data.FirstName;
            found.LastName = data.LastName;
            found.Gender = data.Gender;
            _unitOfWork.Users.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Users>/guid
        [HttpDelete]
        [AuthorizedByRole("Admin")]
        public IActionResult Delete(Guid id)
        {
            Users found = _unitOfWork.Users.GetBySingle(x => x.Id == id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Users.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
