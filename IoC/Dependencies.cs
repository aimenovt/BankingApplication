using BankingApplication.Banking.Domain.CommandHandlers;
using BankingApplication.Banking.Domain.Commands;
using BankingApplication.Bus;
using BankingApplication.Transaction.Domain.EventHandlers;
using BankingApplication.Transaction.Domain.Events;
using BankingApplicaton.Domain;
using BankingData.Context;
using BankingData.Interfaces;
using BankingData.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using TransactionData.Context;
using TransactionData.Interfaces;
using TransactionData.Repositories;

namespace IoC
{
    public class Dependencies
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Data
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<BankingDbContext>();
            services.AddScoped<TransactionDbContext>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<ITransferingRepository, TransferingRepository>();

            //Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferingEventHandler>();

            //Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(s =>
            {
                var scopeFactory = s.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(s.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            services.AddTransient<TransferingEventHandler>();
        }
    }
}
