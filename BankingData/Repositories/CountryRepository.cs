using AutoMapper;
using BankingData.Context;
using BankingData.Dto.Country;
using BankingData.Interfaces;
using BankingData.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BankingData.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CountryRepository(BankingDbContext bankingDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bankingDbContext = bankingDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetAccountId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<bool> CountryExists(int countryId)
        {
            var exists = _bankingDbContext.Countries.Any(c => c.Id == countryId);

            return exists;
        }

        public async Task<ServiceResponse<List<GetCountryDto>>> CreateCountry(AddCountryDto newCountry)
        {
            var countryToAdd = _mapper.Map(newCountry, new Country());
            _bankingDbContext.Countries.Add(countryToAdd);
            await _bankingDbContext.SaveChangesAsync();

            var response = new ServiceResponse<List<GetCountryDto>>();
            response.Data = _mapper.Map(_bankingDbContext.Countries.ToList(), new List<GetCountryDto>());

            return response;
        }

        public async Task<ServiceResponse<List<GetCountryDto>>> DeleteCountry(int countryId)
        {
            var countryToRemove = _bankingDbContext.Countries.Where(c => c.Id == countryId).FirstOrDefault();
            _bankingDbContext.Countries.Remove(countryToRemove);
            await _bankingDbContext.SaveChangesAsync();

            var response = new ServiceResponse<List<GetCountryDto>>();
            response.Data = _mapper.Map(_bankingDbContext.Countries.ToList(), new List<GetCountryDto>());

            return response;
        }

        public async Task<ServiceResponse<List<GetCountryDto>>> GetCountries()
        {
            var response = new ServiceResponse<List<GetCountryDto>>();
            response.Data = _mapper.Map(_bankingDbContext.Countries.ToList(), new List<GetCountryDto>());

            return response;
        }

        public async Task<ServiceResponse<GetCountryDto>> GetCountry(int countryId)
        {
            var countryToGet = _bankingDbContext.Countries.Where(c => c.Id == countryId).FirstOrDefault();

            var response = new ServiceResponse<GetCountryDto>();
            response.Data = _mapper.Map(countryToGet, new GetCountryDto());

            return response;
        }

        public async Task<ServiceResponse<GetCountryDto>> GetCountryOfAccount()
        {
            var countryOfAccount = _bankingDbContext.Accounts.Where(a => a.Id == GetAccountId()).Select(a => a.Country).FirstOrDefault();

            var response = new ServiceResponse<GetCountryDto>();
            response.Data = _mapper.Map(countryOfAccount, new GetCountryDto());

            return response;
        }

        public async Task<ServiceResponse<GetCountryDto>> UpdateCountry(UpdateCountryDto updatedCountry)
        {
            var country = _bankingDbContext.Countries.Where(c => c.Id == updatedCountry.Id).FirstOrDefault();

            _mapper.Map(updatedCountry, country);

            _bankingDbContext.Countries.Update(country);
            await _bankingDbContext.SaveChangesAsync();

            var response = new ServiceResponse<GetCountryDto>();
            response.Data = _mapper.Map(country, new GetCountryDto());

            return response;
        }
    }
}
