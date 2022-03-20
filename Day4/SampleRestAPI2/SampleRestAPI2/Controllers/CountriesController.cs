using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.BLL.Services;
using AutoMapper;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly CountryServices _services;
        private readonly IMapper _mapper;
        public CountriesController(IUnitOfWork unitOfWork)
        {
            _services = new CountryServices(unitOfWork);
            MapperConfiguration config = new MapperConfiguration(m =>
            {
                m.CreateMap<CountriesDTO, Countries>();
                m.CreateMap<Countries, CountriesDTO>();

                m.CreateMap<CountriesWithMerchantDTO, Countries>()
                    .ForMember(s => s.Merchants, opt => opt.Ignore());
                m.CreateMap<Countries, CountriesWithMerchantDTO>();
            });

            _mapper = config.CreateMapper();
        }

        // GET: api/<Countries>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            return Ok(await _services.Get());
        }

        // GET api/<Countries>/5
        [HttpGet("{id}")]
        [Authorize]
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

        // GET api/<Countries>/WithMerchants
        [HttpGet]
        [Route("WithMerchants")]
        [Authorize]
        public async Task<ActionResult> GetWithMerchants(Guid id)
        {
            try
            {
                Countries result = await _services.GetWithMerchants(x => id == x.Id);
                List<CountriesWithMerchantDTO> mappedResult = _mapper.Map<List<CountriesWithMerchantDTO>>(result);
                return Ok(mappedResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // POST api/<Countries>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Countries data)
        {
                _services.Add(data);
                return Ok();
        }

        // PUT api/<Countries>/guid
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] Countries data)
        {
            if (!_services.Update(data))
                return BadRequest();
            return Ok();
        }

        // DELETE api/<Countries>/guid
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            if (!_services.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
