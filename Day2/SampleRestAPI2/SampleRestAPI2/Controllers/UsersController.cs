﻿using Microsoft.AspNetCore.Mvc;
using SampleRestAPI2.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.BLL.Repository;

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
        public async Task<ActionResult> Get()
        {
            IEnumerable<Users> data = await _unitOfWork.Users.GetAll();
            IEnumerable<UsersDTO> result = data.Select(c => new UsersWithCountryDTO
            {
                Id = c.Id,
                CountryId = c.CountryId,
                CountryName = c.Countries.Name,
                DateOfBirth = c.DateOfBirth,
                Email = c.Email,
                FullName = c.FullName,
                Gender = c.Gender
            });
            return Ok(result);
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            Users data = await _unitOfWork.Users.Get(id);
            UsersDTO result = new UsersWithCountryDTO
            {
                Id = data.Id,
                CountryId = data.CountryId,
                CountryName = data.Countries.Name,
                DateOfBirth = data.DateOfBirth,
                Email = data.Email,
                FullName = data.FullName,
                Gender = data.Gender
            };
            return Ok(result);
        }

        // POST api/<Users>
        [HttpPost]
        public IActionResult Post([FromBody] UsersDTO data)
        {
            _unitOfWork.Users.Add(new Users()
            {
                CountryId = data.CountryId,
                CreatedDate = DateTime.Now,
                DateOfBirth = data.DateOfBirth,
                Email = data.Email,
                FullName = data.FullName,
                Gender = data.Gender,
                Id = data.Id
            });
            _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/<Users>/guid
        [HttpPut]
        public IActionResult Put([FromBody] UsersDTO data)
        {
            Users found = _unitOfWork.Users.Get(data.Id).Result;
            if (found == null)
                return BadRequest();

            found.CountryId = data.CountryId;
            found.DateOfBirth = data.DateOfBirth;
            found.Email = data.Email;
            found.FullName = data.FullName;
            found.Gender = data.Gender;
            _unitOfWork.Users.Update(found);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<Users>/guid
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Users found = _unitOfWork.Users.Get(id).Result;
            if (found == null)
                return BadRequest();
            _unitOfWork.Users.Delete(found);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
