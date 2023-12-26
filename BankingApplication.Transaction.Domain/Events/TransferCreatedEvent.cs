using BankingApplicaton.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Transaction.Domain.Events
{
    public class TransferCreatedEvent : Event
    {
        public int FromAccountId { get; private set; }
        public int ToAccountId { get; private set; }
        public decimal TransferAmount { get; private set; }

        public TransferCreatedEvent(int fromaccountid, int toaccountid, decimal transferamount)
        {
            FromAccountId = fromaccountid;
            ToAccountId = toaccountid;
            TransferAmount = transferamount;
        }
    }
}
