using BankingApplicaton.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Banking.Domain.Events
{
    public class TransferCreatedEvent : Event
    {
        public int FromAccountId { get; private set; }
        public int ToAccountId { get; private set; }
        public decimal TransferAmount { get; private set; }

        public TransferCreatedEvent(int from, int to ,decimal amount)
        {
            FromAccountId = from;
            ToAccountId = to;
            TransferAmount = amount;
        }
    }
}
