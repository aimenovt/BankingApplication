using BankingApplication.Transaction.Domain.Events;
using BankingApplicaton.Domain;
using BankingData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Dto;
using TransactionData.Interfaces;

namespace BankingApplication.Transaction.Domain.EventHandlers
{
    public class TransferingEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferingRepository _transferingRepository;

        public TransferingEventHandler(ITransferingRepository transferingRepository)
        {
            _transferingRepository = transferingRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            _transferingRepository.AddTransferLog(new AddTransferLogDto()
            {
                FromAccount = @event.FromAccountId,
                ToAccount = @event.ToAccountId,
                TransferAmount = @event.TransferAmount
            });

            return Task.CompletedTask;  
        }
    }
}
