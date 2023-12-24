using BankingData.Dto.Country;
using BankingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Interfaces
{
    public interface ICountryRepository
    {
        Task<bool> CountryExists(int countryId);
        Task<ServiceResponse<List<GetCountryDto>>> GetCountries();
        Task<ServiceResponse<GetCountryDto>> GetCountry(int countryId);
        Task<ServiceResponse<GetCountryDto>> GetCountryOfAccount();
        Task<ServiceResponse<List<GetCountryDto>>> CreateCountry(AddCountryDto newCountry);
        Task<ServiceResponse<GetCountryDto>> UpdateCountry(UpdateCountryDto updatedCountry);
        Task<ServiceResponse<List<GetCountryDto>>> DeleteCountry(int countryId);
    }
}
