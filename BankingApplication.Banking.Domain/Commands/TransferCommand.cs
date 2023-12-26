using BankingApplicaton.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Banking.Domain.Commands
{
    public abstract class TransferCommand : Command
    {
        public int FromAccountId { get; protected set; }
        public int ToAccountId { get; protected set; }
        public decimal TransferAmount { get; protected set; }
    }
}
