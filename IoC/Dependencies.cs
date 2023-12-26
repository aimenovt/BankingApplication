using BankingApplication.Banking.Domain.CommandHandlers;
using BankingApplication.Banking.Domain.Commands;
using BankingApplication.Bus;
using BankingApplicaton.Domain;
using BankingData.Context;
using BankingData.Interfaces;
using BankingData.Repositories;
using MediatR;
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
            services.AddScoped<ITransferRepository, TransferRepository>();

            //Events

            //Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(s =>
            {
                var scopeFactory = s.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(s.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
        }
    }
}
