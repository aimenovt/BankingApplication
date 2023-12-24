using AutoMapper;
using BankingData.Dto.Country;
using BankingData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-available-countries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _countryRepository.GetCountries());
        }

        [HttpGet("get-country/{countryId}")]
        public async Task<IActionResult> GetCountry(int countryId)
        {
            return Ok(await _countryRepository.GetCountry(countryId));
        }

        [Authorize]
        [HttpGet("get-country-of-account")]
        public async Task<IActionResult> GetCountryOfAccount()
        {
            return Ok(await _countryRepository.GetCountryOfAccount());
        }

        [HttpPost("create-country")]
        public async Task<IActionResult> CreateCountry(AddCountryDto newCountry)
        {
            return Ok(await _countryRepository.CreateCountry(newCountry));
        }

        [HttpPut("update-country")]
        public async Task<IActionResult> UpdateCountry(UpdateCountryDto countryToUpdate)
        {
            return Ok(await _countryRepository.UpdateCountry(countryToUpdate));
        }

        [HttpDelete("delete-country/{countryId}")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            return Ok(await _countryRepository.DeleteCountry(countryId));
        }
    }
}
