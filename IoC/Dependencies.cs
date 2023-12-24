using BankingData.Context;
using BankingData.Interfaces;
using BankingData.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class Dependencies
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Data
            services.AddScoped<BankingDbContext>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
