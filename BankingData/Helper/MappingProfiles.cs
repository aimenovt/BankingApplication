using AutoMapper;
using BankingData.Dto.Account;
using BankingData.Dto.Country;
using BankingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Country
            CreateMap<AddCountryDto, Country>();
            CreateMap<AddCountryDto, Country>().ReverseMap();

            CreateMap<GetCountryDto, Country>();
            CreateMap<GetCountryDto, Country>().ReverseMap();

            CreateMap<UpdateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>().ReverseMap();


            //Account
            CreateMap<RegisterAccountDto, Account>();
            CreateMap<RegisterAccountDto, Account>().ReverseMap();

            CreateMap<GetAccountDto, Account>();
            CreateMap<GetAccountDto, Account>().ReverseMap();

            CreateMap<LoginAccountDto, Account>();
            CreateMap<LoginAccountDto, Account>().ReverseMap();

            CreateMap<UpdateAccountDto, Account>();
            CreateMap<UpdateAccountDto, Account>().ReverseMap();
        }
    }
}
